namespace Shops.Exceptions;

public class NegativeProductCountException : ProductException
{
    public NegativeProductCountException(int productCount)
        : base($"Product count can't be negative ( {productCount} <= 0)")
    {
    }
}