using Banks.Entities.Notification;
using Banks.Entities.Transaction;

namespace Banks.Entities.BackAccount;

public interface IBankAccount
{
    void FillUpMoney(double moneyAmount);
    void TakeOffMoney(double moneyAmount);
    Guid GetAccountId();
    AccountType GetAccountType();
    void ChangeParameter(ChangeType changeType, double newParameter);
    double GetBalance();

    void RemoveTransactionLog(TransactionLog transactionLog);
    void AddTransactionLog(TransactionLog transactionLog);

    List<TransactionLog> GetTransactions();
}