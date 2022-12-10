using Banks.Entities.BackAccount;

namespace Banks.Entities.Transaction;

public class TransactionLog
{
    public TransactionLog(
        IBankAccount accountFrom,
        IBankAccount accountTo,
        double transferAmount,
        TransactionType transactionType)
    {
        TransactionId = Guid.NewGuid();
        AccountFrom = accountFrom;
        AccountTo = accountTo;
        TransferAmount = transferAmount;
        TransactionType = transactionType;
    }

    public Guid TransactionId { get; }
    public IBankAccount AccountFrom { get; }
    public IBankAccount AccountTo { get; }
    public double TransferAmount { get; }
    public TransactionType TransactionType { get; }
}