using DataAccess.Models.Sources;

namespace Presentation.Models.Workers;

public record CreateAccountModel(string login, string password);