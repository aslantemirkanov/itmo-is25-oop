using DataAccess.Models;

namespace Application.Services;

public interface IReportService
{
    List<Message> GetTodayMessages();
}