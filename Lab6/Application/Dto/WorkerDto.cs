using DataAccess.Models;

namespace Application.Dto;

public record WorkerDto(string login, Guid id, int status, List<Account> accounts);
