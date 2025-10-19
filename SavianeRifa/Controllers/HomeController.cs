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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return BadRequest(new { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
