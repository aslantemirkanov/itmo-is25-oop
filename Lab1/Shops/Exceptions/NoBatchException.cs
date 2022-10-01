namespace Shops.Exceptions;

public class NoBatchException : ShopException
{
    public NoBatchException()
        : base("There are not shop which can sell you that batch")
    {
    }
}