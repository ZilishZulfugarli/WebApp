using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
	public class CategoryAddVM
	{
		[Required] public string? CategoryName { get; set; }