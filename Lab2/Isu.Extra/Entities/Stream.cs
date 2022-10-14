using IsuExtra.Exceptions;

namespace Isu.Extra.Entities;

public class Stream
{
    public Stream(int flowNumber, int maxStudentCount, Schedule schedule, Schedule flowSchedule)
    {
        if (maxStudentCount < 0)
        {
            throw new NegativeStudentCountException();
        }

        FlowSchedule = flowSchedule;
        MaxStudentCount = maxStudentCount;
        FlowNumber = flowNumber;
    }

    public Schedule FlowSchedule { get; }
    public int MaxStudentCount { get; }
    public int FlowNumber { get; }
}