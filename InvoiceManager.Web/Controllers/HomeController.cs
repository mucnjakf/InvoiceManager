using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InvoiceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        // GET: Home
        public IActionResult Index()
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
