using System.Net.Mime;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public class Worker
{
    public Worker(string login, string password, Guid id, int status, List<Worker> subWorkers, List<Account> accounts)
    {
        Login = login;
        Password = password;
        Id = id;
        Status = status;
        SubWorkers = subWorkers;
        Accounts = accounts;
    }

#pragma warning disable CS8618
    protected Worker() { }
#pragma warning restore CS8618
    public string Login { get; set; }
    public string Password { get; set; }

    public Guid Id { get; set; }

    public int Status { get; set; }

    public virtual List<Worker> SubWorkers { get; set; }
    public virtual List<Account> Accounts { get; set; }
}