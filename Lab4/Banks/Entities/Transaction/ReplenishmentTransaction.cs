using Banks.Entities.BackAccount;

namespace Banks.Entities.Transaction;

public class ReplenishmentTransaction
{
    public void TransactionExecute(IBankAccount bankAccount, double moneyAmount)
    {
        bankAccount.FillUpMoney(moneyAmount);
    }
}