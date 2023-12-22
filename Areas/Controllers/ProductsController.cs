using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Models;
using WebApp.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly FileService _fileService;

        public ProductsController(AppDbContext dbContext, FileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var products = _dbContext.Products.Include(x => x.ProductImage).Include(x => x.Category).ToList();
            var model = new ProductsVM
            {
                Products = products
            };
            return View(model);
        }

        public IActionResult AddProduct()
        {
            var model = new ProductAddVM();
            var productCategory = _dbContext.Categories.ToList();

            model.productCategory = productCategory.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.Id.ToString()
            }).ToList();
            

            return View(model);
        }

        [HttpPost]

        public IActionResult AddProduct(ProductAddVM model)
        {
            if(!ModelState.IsValid)
            {
                var productCategory = _dbContext.Categories.ToList();

                model.productCategory = productCategory.Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                }).ToList();

                return View(model);
            }

            var imageName = _fileService.UploadFile(model.Photo);
            var product = new Product
            {
                ProductName = model.Name,
                Price = (decimal)model.Price,
                CategoryId = model.CategoryId,
                ProductImage = new ProductImage
                {
                    ImageName = imageName
                }
            };

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) NotFound();

            _dbContext.Remove(product);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateProduct(int id)
        {
            var product = _dbContext.Products.Include(x => x.ProductImage).Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            if (product is null) NotFound();

            var category = _dbContext.Categories.ToList();

            var model = new ProductUpdateVM
            {
                Name = product.ProductName,
                Price = product.Price,
                CategoryId = product.Category.Id,
                ImageName = product.ProductImage.ImageName,
                productCategory = category.Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateProduct(ProductUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var product = _dbContext.Products.Include(x => x.ProductImage).FirstOrDefault(x => x.Id == model.Id);
            if (product is null) return View(model);

            if (model.Photo != null)
            {
                if(product.ProductImage != null)
                {
                    _fileService.DeleteFile(product.ProductImage.ImageName);
                }
                product.ProductImage.ImageName = _fileService.UploadFile(model.Photo);
            }

            product.ProductName = model.Name;
                product.Price = (decimal)model.Price;
                product.CategoryId = model.CategoryId;
            

            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
            
        }
    }
}
