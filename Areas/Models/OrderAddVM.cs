using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Models{
	public class OrderAddVM
	{
		public int ProductId { get; set; }
		public int CustomerId { get; set; }
		public DateTime OrderDate { get; set; }
		public List<SelectListItem>? products { get; set; }
        public List<SelectListItem>? customers { get; set; }
    }
}

