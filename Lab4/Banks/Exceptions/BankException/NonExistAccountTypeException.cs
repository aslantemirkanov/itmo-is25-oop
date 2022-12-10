namespace Banks.Exceptions;

public class NonExistAccountTypeException : BankException
{
    public NonExistAccountTypeException()
        : base("That account type doesn't exist")
    {
    }
}