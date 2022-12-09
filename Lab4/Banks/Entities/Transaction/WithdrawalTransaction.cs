using Banks.Entities.BackAccount;

namespace Banks.Entities.Transaction;

public class WithdrawalTransaction
{
    public void TransactionExecute(IBankAccount bankAccount, double moneyAmount)
    {
        bankAccount.TakeOffMoney(moneyAmount);
    }
}