using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WabAJAX.Models;

namespace WabAJAX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoContext _conetxt;

        public HomeController(ILogger<HomeController> logger, DemoContext conetxt)
        {
            _logger = logger;
            _conetxt = conetxt;
        }

        public IActionResult Index()
        {
            var test =_conetxt.Members.Where(p=>p.MemberId== 1).ToList();
            return View(test);
        }
        public IActionResult FirstAjax()
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