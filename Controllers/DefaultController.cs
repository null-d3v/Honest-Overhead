using Microsoft.AspNetCore.Mvc;

namespace HonestOverhead.Controllers
{
    public class DefaultController : Controller
    {
        [Route("[action]")]
        public IActionResult About()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Error()
        {
            return View();
        }

        [Route("")]
        public IActionResult Main()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Services()
        {
            return View();
        }
    }
}
