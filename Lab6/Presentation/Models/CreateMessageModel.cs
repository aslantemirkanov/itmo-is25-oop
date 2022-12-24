namespace Presentation.Models.Workers;

public record CreateMessageModel(Guid sender, Guid receiver, string message);