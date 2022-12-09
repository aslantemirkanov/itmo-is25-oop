using Banks.Services;

namespace Banks.Entities;

public class Timer
{
    public void RewindTime(int daysCount)
    {
        DateTime dateNow = DateTime.Now;
        Console.WriteLine(DateTime.Now);
        for (int i = 0; i < daysCount; i++)
        {
            CentralBank.GetInstance().AddDayInterest();
            DateTime dateTime = dateNow.AddDays(i + 1);
            if (dateTime.Day.Equals(1))
            {
                CentralBank.GetInstance().AddMonthInterest();
            }

            Console.WriteLine(dateTime);
        }
    }
}