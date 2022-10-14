namespace IsuExtra.Exceptions;

public class OutOfScheduleRangeException : IsuExtraException
{
    public OutOfScheduleRangeException(int classNumber)
        : base($"There are no {classNumber} class in a day ")
    {
    }
}