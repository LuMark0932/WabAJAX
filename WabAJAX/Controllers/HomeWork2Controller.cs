using Microsoft.AspNetCore.Mvc;
using WabAJAX.Models;

namespace WabAJAX.Controllers
{
    public class HomeWork2Controller : Controller
    {
        private readonly DemoContext _conetxt;
        public HomeWork2Controller( DemoContext conetxt)
        {
            _conetxt = conetxt;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
