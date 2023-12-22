namespace WebApp;

public class Entities : Data
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ProductImage ProductImage { get; set; }
    public List<Order> Order { get; set; }
}