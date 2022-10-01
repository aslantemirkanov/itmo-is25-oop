using Shops.Models;

namespace Shops.Exceptions;

public class NonExistShop : ShopException
{
    public NonExistShop()
        : base("There are not enough products in batch")
    {
    }

    public NonExistShop(ShopName shopName)
        : base($"There are no shop {shopName}")
    {
    }
}