using AjaxHMK.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AjaxHMK.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HMK1()
        {
            return View();
        }

        public IActionResult HMK2()
        {
            return View();
        }

        public IActionResult HMK3()
        {
            return View();
        }

        public IActionResult HMK4()
        {
            return View();
        }

        public IActionResult HMK5()
        {
            return View();
        }

        public IActionResult HMK6() 
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