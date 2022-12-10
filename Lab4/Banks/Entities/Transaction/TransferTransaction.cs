using Banks.Entities.BackAccount;

namespace Banks.Entities.Transaction;

public class TransferTransaction
{
    public void TransactionExecute(IBankAccount accountFrom, IBankAccount accountTo, double moneyAmount)
    {
        accountFrom.TakeOffMoney(moneyAmount);
        accountTo.FillUpMoney(moneyAmount);
    }
}