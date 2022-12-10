using System.Threading.Tasks.Dataflow;
using Banks.Entities.BackAccount;

namespace Banks.Exceptions;

public class BankException : Exception
{
    public BankException(string message)
        : base(message)
    {
    }
}