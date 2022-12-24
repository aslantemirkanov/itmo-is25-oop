using DataAccess.Models;

namespace Application.Dto;

public record AccountDto(string login, Guid id, List<Worker> workers);