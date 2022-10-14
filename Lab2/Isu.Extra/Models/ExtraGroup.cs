using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public class ExtraGroup
{
    private List<ExtraStudent> _students;
    private Schedule _schedule;

    public ExtraGroup(Schedule schedule)
    {
        _students = new List<ExtraStudent>();
        _schedule = schedule;
    }
}