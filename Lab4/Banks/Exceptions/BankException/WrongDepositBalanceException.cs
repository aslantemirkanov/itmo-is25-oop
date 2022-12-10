namespace Banks.Exceptions;

public class WrongDepositBalanceException : BankException
{
    public WrongDepositBalanceException(double balance)
        : base($"You can't create deposit account with balance {balance}, because bank haven't that rate")
    {
    }
}