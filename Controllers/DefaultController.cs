using Microsoft.AspNetCore.Mvc;

namespace HonestOverhead.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Main()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }
    }
}
