using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class CustomersController : Controller
    {
        private AppDbContext _dbContext;

        public CustomersController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var customer = _dbContext.Customers.Include(x => x.Orders).ToList();
            var model = new CustomersVM()
            {
                Customers = customer
            };

            return View(model);
        }

        public IActionResult AddCustomer()
        {
            var model = new CustomerAddVM();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerAddVM model)
        {
            if (!ModelState.IsValid) return NotFound();
            var customer = new Customer
            {
                Name = model.FirstName,
                Surname = model.Lastname,
                Email = model.Email
            };

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(x => x.Id == id);

            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateCustomer(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(x => x.Id == id);
            if (customer == null) NotFound();

            var model = new CustomerUpdateVM
            {
                FirstName = customer.Name,
                Lastname = customer.Surname,
                Email = customer.Email
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateCustomer(CustomerUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var customer = _dbContext.Customers.FirstOrDefault(x => x.Id == model.Id);

            customer.Name = model.FirstName;
            customer.Surname = model.Lastname;
            customer.Email = model.Email;

            _dbContext.Customers.Update(customer);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

