using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestoranMVCMPA201.Contexts;
using RestoranMVCMPA201.ViewModels.FoodViewModels;

namespace RestoranMVCMPA201.Controllers;

public class MenuController : Controller
{
    private readonly SimulationDbContext _context;

    public MenuController(SimulationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var foods = await _context.Foods.Select(x => new FoodGetVM()
        {
            Name=x.Name,
            Description=x.Description,
            CategoryName=x.Category.Name,
            ImagePath=x.ImagePath
        }).ToListAsync();
        return View(foods);
    }
}
