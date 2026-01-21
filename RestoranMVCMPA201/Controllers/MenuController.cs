using Microsoft.AspNetCore.Mvc;

namespace RestoranMVCMPA201.Controllers;

public class MenuController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
