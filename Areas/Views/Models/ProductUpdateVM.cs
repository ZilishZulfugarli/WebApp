using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace admin_panel.Models
{
	public class ProductUpdateVM
	{
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [MinLength(5, ErrorMessage = "Name can't be less than 5 characters!")]
        [MaxLength(10, ErrorMessage = "Name can't be more than 10 characters!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required!")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Price can't be less than 0!")]
        public decimal? Price { get; set; }

        public string? ImageName { get; set; }
        public IFormFile? Photo { get; set; }
        [Required(ErrorMessage = "Please select product category")]
        public int CategoryId { get; set; }
        public List<SelectListItem>? productCategory { get; set; }
    }
}

