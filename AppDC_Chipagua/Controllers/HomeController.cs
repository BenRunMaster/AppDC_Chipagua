using System.Diagnostics;
using AppDC_Chipagua.Models;
using AppDC_Chipagua.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AppDC_Chipagua.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReclamoService _reclamoService;



        public HomeController(ILogger<HomeController> logger, ReclamoService reclamoService)
        {
            _logger = logger;
            _reclamoService = reclamoService;
        }

        public async Task<IActionResult>  Index()
        {
            List<Reclamo> reclamos = await _reclamoService.GetReclamosAsync();
            return View(reclamos);
        }

        [HttpGet]
        public IActionResult CreateReclamo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateReclamo(Reclamo reclamo)
        {

            if (!ModelState.IsValid)
            {
                return View(reclamo);
            }

            Console.WriteLine(reclamo.NombreProveedor);
            Console.WriteLine(reclamo.DireccionProveedor);
            Console.WriteLine(reclamo.NombresConsumidor);
            Console.WriteLine(reclamo.ApellidosConsumidor);
            Console.WriteLine(reclamo.DUI);
            Console.WriteLine(reclamo.DetalleReclamo);
            Console.WriteLine(reclamo.MontoReclamado);
            Console.WriteLine(reclamo.Telefono);
            Console.WriteLine(reclamo.FechaIngreso);
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
