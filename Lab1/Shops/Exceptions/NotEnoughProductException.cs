using Shops.Models;

namespace Shops.Exceptions;

public class NotEnoughProductException : ProductException
{
    public NotEnoughProductException()
        : base("There are not enough products in batch")
    {
    }

    public NotEnoughProductException(int productCount, ProductName productName)
        : base($"There are no {productCount} {productName}")
    {
    }
}