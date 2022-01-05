using Microsoft.AspNetCore.Mvc;

namespace BackendAPIs.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
