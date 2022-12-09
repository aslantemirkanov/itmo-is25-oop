using Banks.Entities;

namespace Banks.Exceptions;

public class ClientException : Exception
{
    private ClientException(string message)
        : base(message)
    {
    }

    public static ClientException WrongPassportSeriesException(string passportSeries)
    {
        return new ClientException($"Passport series {passportSeries} is wrong");
    }

    public static ClientException WrongPhoneNumberException(string phoneNumber)
    {
        return new ClientException($"Phone number {phoneNumber} is wrong");
    }

    public static ClientException TryToGetNullPhoneNumber(Client client)
    {
        return new ClientException($"{client.GetName()} don't have phone number");
    }
}