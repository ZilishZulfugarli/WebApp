﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
	public class CategoryUpdateVM
	{
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}

