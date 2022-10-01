namespace Shops.Models;

public class ShopAddress
{
    public ShopAddress(string shopAddress)
    {
        Address = shopAddress;
    }

    public string Address { get; }
}