using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
	public class OrderUpdateVM
	{
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public List<SelectListItem>? products { get; set; }
        public List<SelectListItem>? customers { get; set; }
    }
}

