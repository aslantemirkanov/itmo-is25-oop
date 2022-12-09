using Banks.Entities.Notification;

namespace Banks.Entities.BackAccount;

public interface IBankAccount
{
    void FillUpMoney(double moneyAmount);
    void TakeOffMoney(double moneyAmount);
    Guid GetAccountId();
    AccountType GetAccountType();
    void ChangeParameter(ChangeType changeType, double newParameter);
    double GetBalance();
}