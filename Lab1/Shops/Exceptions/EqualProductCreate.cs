using Shops.Models;

namespace Shops.Exceptions;

public class EqualProductCreate : ProductException
{
    public EqualProductCreate(ProductName productNameName)
        : base($"Product {productNameName} is already exist")
    {
    }
}