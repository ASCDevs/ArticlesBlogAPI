using Microsoft.AspNetCore.Mvc;

namespace ArticlesBlogAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
