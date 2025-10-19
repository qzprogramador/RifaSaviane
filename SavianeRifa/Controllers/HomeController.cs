using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SavianeRifa.Models;
using SavianeRifa.Services;

namespace SavianeRifa.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/pix")]
        public IActionResult Pix(double total)
        {
            string valorPix = "";
            try
            {
                valorPix = double.Parse(total.ToString("0.00")).ToString().Replace(",", ".");

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

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
