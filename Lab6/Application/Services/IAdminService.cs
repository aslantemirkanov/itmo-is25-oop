using Application.Dto;
using DataAccess.Models;
using DataAccess.Models.Sources;

namespace Application.Services;

public interface IAdminService
{
    AccountDto CreateAccount(string login, string password);
    void AddWorkerToAccount(Guid accountId, Guid workerId);
    public void AddSource(SourceType sourceType, Guid accountId, string message);
}