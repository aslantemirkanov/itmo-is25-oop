using Banks.Exceptions;

namespace Banks.Models;

public class Passport
{
    private string _passportSeries;
    public Passport(string passportSeries)
    {
        if (passportSeries.Length != 10 || !long.TryParse(passportSeries, out long _))
        {
            throw new WrongPassportSeriesException(passportSeries);
        }

        _passportSeries = passportSeries;
    }

    public string GetPassport()
    {
        return _passportSeries;
    }
}