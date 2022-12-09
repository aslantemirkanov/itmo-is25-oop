using Banks.Entities;
using Banks.Entities.Notification;
using Banks.Models;
using Banks.Services;

namespace Banks.Console
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            /*
                        var client = new Client.ClientBuilder()
                            .AddName("lele", "lel")
                            .AddPassport("12345678")
                            .Build();
                        client.AddPassport("12456");
            var not = new MailNotification(new PhoneNotification());
            not.Send();
            var phone = new PhoneNumber("89969174661");
            var bank = new Bank();
            var client = bank.AddClient("aslan", "temirkanov", null, new PhoneNumber("89969174661"));*/
            CentralBank centralBank = CentralBank.GetInstance();
        }
    }
}