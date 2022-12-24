using DataAccess.Models;
using DataAccess.Models.Sources;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Worker> Workers { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<PhoneSource> PhoneSources { get; set; } = null!;
    public DbSet<MailSource> MailSources { get; set; } = null!;
    public DbSet<MessengerSource> MessengerSources { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;

    public Worker? GetWorker(string login, string password)
    {
        Worker? worker = Workers.ToList().FirstOrDefault(u =>
            u.Login.Equals(login) && u.Password.Equals(password));
        return worker;
    }

    public Worker? GetWorker(Guid id)
    {
        Worker? worker = Workers.ToList().FirstOrDefault(u =>
            u.Id.Equals(id));
        return worker;
    }

    public Account? GetAccount(Guid id)
    {
        Account? account = Accounts.ToList().FirstOrDefault(u =>
            u.Id.Equals(id));
        return account;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(builder =>
        {
            builder.HasMany(x => x.Workers).WithMany(x => x.Accounts);
        });
        modelBuilder.Entity<Worker>(builder =>
        {
            builder.HasMany(x => x.Accounts).WithMany(x => x.Workers);
        });
        modelBuilder.Entity<Worker>(builder =>
        {
            builder.HasMany(x => x.SubWorkers);
        });
        modelBuilder.Entity<Account>(builder =>
        {
            builder.HasMany(x => x.MailSources);
        });
        modelBuilder.Entity<Account>(builder =>
        {
            builder.HasMany(x => x.PhoneSources);
        });
        modelBuilder.Entity<Account>(builder =>
        {
            builder.HasMany(x => x.MessengerSources);
        });
    }
}