using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Entities;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private AppDbContext _dbContext;

        public OrdersController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var orders = _dbContext.Orders.Include(x => x.Product).Include(x => x.Customer).ToList();
            var model = new OrdersVM
            {
                Orders = orders
            };
            return View(model);
        }

        public IActionResult AddOrder()
        {
            var model = new OrderAddVM();
            var product = _dbContext.Products.ToList();

            model.products = product.Select(x => new SelectListItem
            {
                Text = x.ProductName,
                Value = x.Id.ToString()
            }).ToList();

            var customer = _dbContext.Customers.ToList();
            model.customers = customer.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddOrder(OrderAddVM model)
        {
            if (!ModelState.IsValid)
            {

                var product = _dbContext.Products.ToList();

                model.products = product.Select(x => new SelectListItem
                {
                    Text = x.ProductName,
                    Value = x.Id.ToString()
                }).ToList();

                var customer = _dbContext.Customers.ToList();
                model.customers = customer.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

                return View(model);
            }

            var order = new Order
            {
                CustomerId = model.CustomerId,
                ProductId = model.ProductId,
                OrderDate = DateTime.Now
            };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.Id == id);

            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateOrder(int id)
        {
            var model = new OrderUpdateVM();
            var order = _dbContext.Orders.Include(x => x.Customer).Include(x => x.Product).FirstOrDefault(x => x.Id == id);

            var customer = _dbContext.Customers.ToList();

            model.customers = customer.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            var product = _dbContext.Products.ToList();

            model.products = product.Select(x => new SelectListItem
            {
                Text = x.ProductName,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateOrder(OrderUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var order = _dbContext.Orders.Include(x => x.Customer).Include(x => x.Product).FirstOrDefault(x => x.Id == model.Id);
            if (order is null) return View(model);

            order.CustomerId = model.CustomerId;
                order.ProductId = model.ProductId;
                order.OrderDate = DateTime.Now;
            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

