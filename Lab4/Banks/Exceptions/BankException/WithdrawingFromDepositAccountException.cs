namespace Banks.Exceptions;

public class WithdrawingFromDepositAccountException : BankException
{
    public WithdrawingFromDepositAccountException()
        : base("You cant withdraw money from deposit account while it's exist")
    {
    }
}