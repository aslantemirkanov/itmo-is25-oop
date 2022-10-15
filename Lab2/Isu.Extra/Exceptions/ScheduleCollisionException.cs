namespace IsuExtra.Exceptions;
public class ScheduleCollisionException : IsuExtraException
{
    public ScheduleCollisionException()
        : base($"There are schedule collision")
    {
    }
}