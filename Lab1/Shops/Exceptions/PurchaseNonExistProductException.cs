using Shops.Models;

namespace Shops.Exceptions;

public class PurchaseNonExistProductException : ProductException
{
    public PurchaseNonExistProductException()
        : base("You are trying to buy a non-exist product")
    {
    }

    public PurchaseNonExistProductException(ProductName productName)
        : base($"You are trying to buy a non-exist product {productName}")
    {
    }
}