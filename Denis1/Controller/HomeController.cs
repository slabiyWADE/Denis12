using Microsoft.AspNetCore.Mvc;

namespace Denis1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
