using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RestoranMVCMPA201.Models;

namespace RestoranMVCMPA201.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
