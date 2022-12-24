using DataAccess;
using DataAccess.Models;

namespace Application.Services.Implementations;

public class ReportService : IReportService
{
    private readonly DatabaseContext _context;

    public ReportService(DatabaseContext context)
    {
        _context = context;
    }

    public List<Message> GetTodayMessages()
    {
        return _context.Messages.ToList();
        /*return _context.Messages.Where(message => message.CreationTime.DayOfYear.Equals(DateTime.Now.DayOfYear)).ToList();*/
    }
}