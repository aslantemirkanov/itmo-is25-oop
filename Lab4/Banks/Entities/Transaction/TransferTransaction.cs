using Banks.Entities.BackAccount;

namespace Banks.Entities.Transaction;

public class TransferTransaction
{
    public void TransactionExecute(IBankAccount accountFromId, IBankAccount accountToId, double moneyAmount)
    {
        accountFromId.TakeOffMoney(moneyAmount);
        accountToId.FillUpMoney(moneyAmount);
    }
}