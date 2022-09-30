namespace Shops.Entities;

using Shops.Models;

public class Product
{
    public Product(ProductName productName, int productPrice)
    {
        ProductName = productName;
        ProductPrice = productPrice;
    }

    public ProductName ProductName { get; }
    public int ProductPrice { get; set; }
}