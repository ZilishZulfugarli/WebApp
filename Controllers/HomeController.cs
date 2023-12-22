using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;
        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page)
        {
            var product = _dbContext.Products.Include(x => x.ProductImage).Include(x => x.Category).ToList();

            var model = new ProductIndexVM
            {
                Products = product
            };

            return View(model);
        }
    }