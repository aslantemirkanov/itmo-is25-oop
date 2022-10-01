using Shops.Exceptions;

namespace Shops.Entities;

using Shops.Models;

public class Product
{
    public Product(ProductName productName)
    {
        ProductName = productName;
    }

    public ProductName ProductName { get; }
}