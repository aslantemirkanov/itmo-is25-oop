using Banks.Entities;

namespace Banks.Exceptions;

public class TryToGetNullPhoneNumberException : ClientException
{
    public TryToGetNullPhoneNumberException(Client client)
        : base($"{client.GetName()} don't have phone number")
    {
    }
}