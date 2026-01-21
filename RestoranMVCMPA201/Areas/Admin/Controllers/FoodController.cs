using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestoranMVCMPA201.Contexts;
using RestoranMVCMPA201.Helpers;
using RestoranMVCMPA201.Models;
using RestoranMVCMPA201.ViewModels.FoodViewModels;

namespace RestoranMVCMPA201.Areas.Admin.Controllers;
[Area("Admin")]
//[Authorize(Roles ="Admin")]
public class FoodController : Controller
{
    private readonly SimulationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string _folderPath;

    public FoodController(SimulationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
        _folderPath = Path.Combine(_environment.WebRootPath, "img");
    }

    public async Task<IActionResult> Index()
    {
        var foods = await _context.Foods.Select(x => new FoodGetVM()
        {
            Id=x.Id,
            Name=x.Name,
            Description=x.Description,
            CategoryName=x.Category.Name,
            ImagePath=x.ImagePath
        }).ToListAsync();
        return View(foods);
    }

    public async Task<IActionResult> Create()
    {
        await SendCategoriesWithViewBag();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(FoodCreateVM vm)
    {
        await SendCategoriesWithViewBag();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var existCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);
        if (!existCategory)
        {
            ModelState.AddModelError("", "Category not found");
            return View(vm);
        }

        if (!vm.Image.CheckSize(2))
        {
            ModelState.AddModelError("", "Max 2 mb");
            return View(vm);
        }

        if (!vm.Image.CheckType("image"))
        {
            ModelState.AddModelError("", "Image must be in correct form");
            return View(vm);
        }

        var uniqueName = await vm.Image.UploadFileAsync(_folderPath);

        Food food = new Food()
        {
            Name=vm.Name,
            Description=vm.Description,
            CategoryId=vm.CategoryId,
            ImagePath=uniqueName
        };

        await _context.Foods.AddAsync(food);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        var food = await _context.Foods.FindAsync(id);
        if (food == null)
        {
            return NotFound();
        }

        FoodUpdateVM vm = new FoodUpdateVM()
        {
            Id = food.Id,
            Name = food.Name,
            Description = food.Description,
            CategoryId = food.CategoryId
        };

        await SendCategoriesWithViewBag();

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(FoodUpdateVM vm)
    {
        await SendCategoriesWithViewBag();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var food = await _context.Foods.FirstOrDefaultAsync(x => x.Id == vm.Id);
        if (food == null)
        {
            return NotFound();
        }

        var existCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);
        if (!existCategory)
        {
            ModelState.AddModelError("", "Category not found");
            return View(vm);
        }

        if (!vm.Image?.CheckSize(2)??false)
        {
            ModelState.AddModelError("", "Max 2 mb");
            return View(vm);
        }

        if (!vm.Image?.CheckType("image")??false)
        {
            ModelState.AddModelError("", "Image must be in correct form");
            return View(vm);
        }

        food.Name = vm.Name;
        food.Description = vm.Description;
        food.CategoryId = vm.CategoryId;

        if(vm.Image is { })
        {
            var uniqueName = await vm.Image.UploadFileAsync(_folderPath);
            var deletedpath = Path.Combine(_folderPath, food.ImagePath);
            FileHelper.DeleteFile(deletedpath);
            food.ImagePath = uniqueName;
        }

        _context.Foods.Update(food);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> Delete(int id)
    {
        var food = await _context.Foods.FindAsync(id);
        if (food == null)
        {
            return NotFound();
        }

        _context.Foods.Remove(food);
        await _context.SaveChangesAsync();

        var deletedPath = Path.Combine(_folderPath, food.ImagePath);
        FileHelper.DeleteFile(deletedPath);

        return RedirectToAction(nameof(Index));
    }

    private async Task SendCategoriesWithViewBag()
    {
        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = categories;
    }
}
