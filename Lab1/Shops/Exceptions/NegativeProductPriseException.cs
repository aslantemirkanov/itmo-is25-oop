namespace Shops.Exceptions;

public class NegativeProductPriseException : ProductException
{
    public NegativeProductPriseException(decimal productPrice)
        : base($"Product price can't be negative ( {productPrice} < 0)")
    {
    }
}