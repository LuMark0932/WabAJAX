using Microsoft.AspNetCore.Mvc;

namespace WabAJAX.Controllers
{
    public class HomeWorkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
