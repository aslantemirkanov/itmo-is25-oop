using Application.Dto;
using Application.Exceptions;
using Application.Exceptions.NotFound;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;
using DataAccess.Models.Sources;

namespace Application.Services.Implementations;

public class AdminService : IAdminService
{
    private readonly DatabaseContext _context;

    public AdminService(DatabaseContext context)
    {
        _context = context;
    }

    public AccountDto CreateAccount(string login, string password)
    {
        var account = new Account(login, password, Guid.NewGuid());

        _context.Accounts.Add(account);
        _context.SaveChanges();

        return account.AsDto();
    }

    public void AddWorkerToAccount(Guid accountId, Guid workerId)
    {
        var worker = _context.GetWorker(workerId);
        var account = _context.GetAccount(accountId);
        if (worker == null || account == null)
        {
            throw new NotFoundException("entity not found");
        }

        account.Workers.Add(worker);
        worker.Accounts.Add(account);
        _context.SaveChanges();
    }

    public void AddSource(SourceType sourceType, Guid accountId, string message)
    {
        var account = _context.GetAccount(accountId);
        switch (sourceType)
        {
            case SourceType.Mail:
                account?.MailSources.Add(new MailSource(message));
                break;
            case SourceType.Phone:
                account?.PhoneSources.Add(new PhoneSource(message));
                break;
            case SourceType.Messenger:
                account?.MessengerSources.Add(new MessengerSource(message));
                break;
        }
    }
}