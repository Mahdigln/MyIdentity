using Microsoft.AspNetCore.Mvc;
using MyIdentity.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace MyIdentity.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Authorize(Policy = "Buyer")]
        public string JustBuyer()
        {
            return "شما خریدار هستید";
        }

        [Authorize(Policy = "BloodType")]
        public string Blood()
        {
            return "Ap and O";
        }



    }
}