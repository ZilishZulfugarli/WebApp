using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApp.Areas.Models;

namespace WebApp.Controllers;

public class CategoriesController : Controller
{
    private AppDbContext _dbContext;

    public CategoriesController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IActionResult Index()
    {
        var categories = _dbContext.Categories.Include(x => x.Products).ToList();
        var model = new CategoriesVM
        {
            Categories = categories
        };
        return View(model);
    }

    public IActionResult AddCategory()
    {
        var model = new CategoryAddVM();

        return View(model);
    }
    [HttpPost]
    public IActionResult AddCategory(CategoryAddVM model)
    {
        if (!ModelState.IsValid) return View(model);

        var category = new Category
        {
            CategoryName = model.CategoryName
        };

        _dbContext.Categories.Add(category);
        _dbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);

        _dbContext.Categories.Remove(category);
        _dbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update(int id)
    {
        var model = new CategoryUpdateVM();

        return View(model);
    }

    [HttpPost]
    public IActionResult Update(CategoryUpdateVM model)
    {
        if (!ModelState.IsValid) return View(model);

        var category = _dbContext.Categories.FirstOrDefault(x => x.Id == model.Id);

        category.CategoryName = model.Name;
        

        _dbContext.Categories.Update(category);
        _dbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
    

    
}

