using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WabAJAX.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index(string name)
        {
            System.Threading.Thread.Sleep(5000);
            if (string.IsNullOrEmpty(name))
                name = "Ajax";
            return Content($"Hellow {name}","text/plain",Encoding.UTF8);
        }
    }
}
