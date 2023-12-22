using Microsoft.EntityFrameworkCore;
namespace WebApp.Entities;

public class Category
{
    public int Id { get; set; }
    public string? CategoryName { get; set; }
    public List<Product>? Products { get; set; }
}