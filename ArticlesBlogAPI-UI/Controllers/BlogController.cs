using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ArticlesBlogAPI_UI.Models;

namespace ArticlesBlogAPI_UI.Controllers;

public class BlogController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public BlogController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Autores()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogInformation("");
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}