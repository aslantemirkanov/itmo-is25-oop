namespace Banks.Exceptions;

public class NonExistChangeTypeException : BankException
{
    public NonExistChangeTypeException()
        : base("You want to change something strange")
    {
    }
}