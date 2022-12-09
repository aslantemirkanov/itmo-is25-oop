using System.Diagnostics;
using Banks.Entities.BackAccount;
using Banks.Exceptions;
using Banks.Services;

namespace Banks.Entities.Transaction;

public class Transacation
{
    private static Transacation? _instance;
    private List<TransactionLog> _transactions;
    private TransferTransaction _transfer;
    private ReplenishmentTransaction _replenishment;
    private WithdrawalTransaction _withdrawal;

    private Transacation()
    {
        _transactions = new List<TransactionLog>();
        _transfer = new TransferTransaction();
        _replenishment = new ReplenishmentTransaction();
        _withdrawal = new WithdrawalTransaction();
    }

    public static Transacation GetInstance()
    {
        return _instance ??= new Transacation();
    }

    public void ExecuteTransaction(
        TransactionType transactionType,
        double money,
        IBankAccount accountFrom,
        IBankAccount? accountTo = null)
    {
        switch (transactionType)
        {
            case TransactionType.Replenishment:
                _replenishment.TransactionExecute(accountFrom, money);
                _transactions.Add(new TransactionLog(
                    accountFrom,
                    accountFrom,
                    money,
                    TransactionType.Replenishment));
                break;
            case TransactionType.Withdrawal:
                _withdrawal.TransactionExecute(accountFrom, money);
                _transactions.Add(new TransactionLog(
                    accountFrom,
                    accountFrom,
                    money,
                    TransactionType.Replenishment));
                break;
            case TransactionType.Transfer:
                if (accountTo == null)
                {
                    throw new Exception();
                }

                _transfer.TransactionExecute(accountFrom, accountTo, money);
                _transactions.Add(new TransactionLog(
                    accountFrom,
                    accountTo,
                    money,
                    TransactionType.Replenishment));
                break;
            default:
                throw BankException.NonExistTransaction();
        }
    }

    public void Undo(Guid transactionId)
    {
        foreach (TransactionLog log in _transactions.Where(log => log.TransactionId.Equals(transactionId)))
        {
            switch (log.TransactionType)
            {
                case TransactionType.Replenishment:
                    log.AccountFrom.TakeOffMoney(log.TransferAmount);
                    _transactions.Remove(log);
                    break;
                case TransactionType.Withdrawal:
                    log.AccountFrom.FillUpMoney(log.TransferAmount);
                    _transactions.Remove(log);
                    break;
                case TransactionType.Transfer:
                    log.AccountTo.TakeOffMoney(log.TransferAmount);
                    log.AccountFrom.FillUpMoney(log.TransferAmount);
                    _transactions.Remove(log);
                    break;
                default:
                    throw BankException.NonExistTransaction();
            }
        }
    }
}