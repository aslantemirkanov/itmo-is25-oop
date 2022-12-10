namespace Banks.Exceptions;

public class ExtraWithdrawalLimitException : BankException
{
    public ExtraWithdrawalLimitException(double limit)
        : base($"You can't take off more than limit {limit}")
    {
    }
}