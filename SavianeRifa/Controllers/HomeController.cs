using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SavianeRifa.Data;
using Microsoft.EntityFrameworkCore;
using SavianeRifa.Models;
using SavianeRifa.Services;

namespace SavianeRifa.Controllers
{
    public class HomeController : Controller
    {

        

        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        [HttpGet("/pix")]
        public IActionResult Pix(double total)
        {
            try
            {
                string valorPix = double.Parse(total.ToString("0.00")).ToString().Replace(",", ".");

                var pix = new Payload(
                nome: "Saviane Da Silva de Souza",
                chavepix: "04143373289",
                valor: valorPix,
                cidade: "MANAUS",
                txtId: "6909009062345"
            );
            
            var qrcodeBase64 = pix.GerarPayload();
            return Ok(new { Pix = pix.PixCopiaCola, QrCodeBase64 = qrcodeBase64 });

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        public IActionResult Index()
        {
            return View();
        }

         [HttpPost]
    public async Task<IActionResult> RegistrarCompra(string Nome, string Telefone, string Email, IFormFile Comprovante)
    {
        // 1. Salvar o comprovante em disco ou banco
        string filePath = Path.Combine("wwwroot/comprovantes", Comprovante.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await Comprovante.CopyToAsync(stream);
        }

        // 2. Registrar no banco (exemplo simplificado)
        var compra = new PaymentInformation
        {
            Name = Nome,
            PhoneNumber = Telefone,
            Email = Email,
            RegisteredAt = DateTime.Now
        };

        // TODO: salvar no banco via Entity Framework
        // _context.Compras.Add(compra);
        // await _context.SaveChangesAsync();

        // 3. Retornar página de confirmação
        return View("Confirmacao", compra);
    }

        [HttpGet("/rifas")]
        public IActionResult Rifas(int page = 1, int pageSize = 50)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 100;

            var totalCount = _context.Rifas.Count();

            var items = _context.Rifas
                .AsNoTracking()
                .OrderBy(r => r.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new {
                    id = r.Id,
                    number = r.Number,
                    status = r.Status,
                    price = r.Price
                })
                .ToList();

            return Json(new { items, totalCount });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("/reservas")]
        public IActionResult Reservations(string? q)
        {
            // search by name or email (email used only for search, not exposed)
            var query = _context.PaymentInformations.AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                var term = q.Trim().ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(term) || p.Email.ToLower().Contains(term));
            }

            var list = query
                .OrderByDescending(p => p.RegisteredAt)
                .Select(p => new SavianeRifa.Models.ReservationListItem
                {
                    Id = p.Id,
                    Name = p.Name,
                    RegisteredAt = p.RegisteredAt,
                    Location = p.Location,
                    TotalRifas = p.Rifas.Count,
                    ReservedCount = p.Rifas.Count(r => r.Status == "Reservada"),
                    SoldCount = p.Rifas.Count(r => r.Status == "Vendida"),
                    Status = p.Rifas.Any(r => r.Status == "Reservada") ? "Reservada" : (p.Rifas.Any(r => r.Status == "Vendida") ? "Vendida" : "Nenhuma")
                })
                .ToList();

            ViewData["Query"] = q ?? string.Empty;
            return View("Reservations", list);
        }

        [HttpPost("/submit-payment")]
        [RequestSizeLimit(10_000_000)] // allow uploads up to ~10MB
        public async Task<IActionResult> SubmitPayment([FromForm] string name, [FromForm] string phone, [FromForm] string email,
            [FromForm] string pixCopy, [FromForm] string selectedRifas, [FromForm] decimal amount, IFormFile? comprovante, [FromForm] string location)
        {
            // validate basic fields
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Nome, telefone e email são obrigatórios.");
                return BadRequest(ModelState);
            }

            // parse selected rifas (can be comma-separated ids or JSON array)
            var rifasIds = new List<int>();
            if (!string.IsNullOrWhiteSpace(selectedRifas))
            {
                try
                {
                    // try JSON array first
                    if (selectedRifas.TrimStart().StartsWith("["))
                    {
                        rifasIds = System.Text.Json.JsonSerializer.Deserialize<List<int>>(selectedRifas) ?? new List<int>();
                    }
                    else
                    {
                        rifasIds = selectedRifas.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => int.TryParse(s.Trim(), out var id) ? id : 0)
                            .Where(id => id > 0)
                            .ToList();
                    }
                }
                catch
                {
                    // ignore parse errors and continue with empty list
                    rifasIds = new List<int>();
                }
            }

            // save uploaded file (if any)
            string? savedFilePath = null;
            if (comprovante != null && comprovante.Length > 0)
            {
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);
                var fileName = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(comprovante.FileName);
                var filePath = Path.Combine(uploadsPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await comprovante.CopyToAsync(stream);
                }
                // relative path for web access
                savedFilePath = "/uploads/" + fileName;
            }

            // create PaymentInformation record
            var payment = new PaymentInformation
            {
                Name = name,
                PhoneNumber = phone,
                Email = email,
                PixCopyPaste = pixCopy ?? string.Empty,
                Amount = amount,
                RegisteredAt = DateTime.Now,
                ComprovantePath = savedFilePath ?? string.Empty,
                Location = location

            };

            _context.PaymentInformations.Add(payment);
            await _context.SaveChangesAsync();

            // mark rifas as reservada and link to payment
            if (rifasIds.Any())
            {
                var rifasToUpdate = _context.Rifas.Where(r => rifasIds.Contains(r.Id)).ToList();
                foreach (var r in rifasToUpdate)
                {
                    r.PaymentInformationId = payment.Id;
                    r.Status = "Reservada";
                }
                await _context.SaveChangesAsync();
            }

            // prepare view model for confirmation
            var reserved = _context.Rifas
                .Where(r => r.PaymentInformationId == payment.Id)
                .Select(r => new { r.Id, r.Number, r.Price, r.Status })
                .ToList();

            ViewData["Payment"] = payment;
            ViewData["ReservedRifas"] = reserved;

            return View("Confirmation");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return BadRequest(new { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
