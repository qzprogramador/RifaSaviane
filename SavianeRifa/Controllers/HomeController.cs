using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SavianeRifa.Models;
using SavianeRifa.Services;

namespace SavianeRifa.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/pix")]
        public IActionResult Pix()
        {
            var pix = new Payload(
                nome: "ANDREY COSTA DE QUEIROZ",
                chavepix: "02459626207",
                valor: "500.00",
                cidade: "MANAUS",
                txtId: "6909009062345"
            );
            pix.GerarPayload();
            return Ok(pix.PixCopiaCola);
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
