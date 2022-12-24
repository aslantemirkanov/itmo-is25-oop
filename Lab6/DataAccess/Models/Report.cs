namespace DataAccess.Models;

public class Report
{
    public Report(DateTime from, DateTime to, int messageCount)
    {
        From = from;
        To = to;
        MessageCount = messageCount;
    }

    public DateTime From { get; }
    public DateTime To { get; }
    public int MessageCount { get; }
}