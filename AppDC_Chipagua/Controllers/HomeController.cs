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
        
        public async Task<IActionResult>  Index(string direccionMonto = "Mayor a", decimal monto = 25)
        {
            if (monto < 0)
            {
                TempData["MensajeError"] = "El monto no puede ser menor a 0.";
                return RedirectToAction("Index");
            }
            ViewBag.DireccionMonto = direccionMonto;
            ViewBag.Monto = monto;
            List<Reclamo> reclamos = await _reclamoService.GetReclamosAsync(direccionMonto, monto);
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

            bool isDUIExist = await _reclamoService.GetDUIExistAsync(reclamo.DUI);
            if (isDUIExist) {
                ModelState.AddModelError("DUI", "El DUI ya esta resgistrado");
                return View(reclamo);
            }
            try
            {
                await _reclamoService.AddReclamoAsync(reclamo);
                TempData["MensajeExito"] = "�El reclamo se registro con exito!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.ToString());
               ModelState.AddModelError(String.Empty, "Ha ocurrido un error al ingresar el reclamo, intentalo mas tarde");
               return View(reclamo);
            }
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
