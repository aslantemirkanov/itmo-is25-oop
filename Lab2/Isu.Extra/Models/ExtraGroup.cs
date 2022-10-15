using Isu.Entities;
using Isu.Extra.Entities;
using IsuExtra.Exceptions;

namespace Isu.Extra.Models;

public class ExtraGroup
{
    public ExtraGroup(Group group, Schedule schedule)
    {
        Group = group;
        Schedule = schedule;
        ExtraStudents = new List<ExtraStudent>();
        for (int i = 0; i < group.GetStudentList().Count; ++i)
        {
            ExtraStudents.Add(new ExtraStudent(group.GetStudentList()[i], schedule));
        }
    }

    public Group Group { get; }
    public Schedule Schedule { get; }
    public List<ExtraStudent> ExtraStudents { get; }
    public int MaxStudentCount { get; set; } = 30;
    public void AddExtraStudent(ExtraStudent extraStudent)
    {
        if (Group.GetStudentList().Contains(extraStudent.Student))
        {
            throw new ExtraStudentAlreadyAddedException(extraStudent);
        }

        if (ExtraStudents.Count + 1 > MaxStudentCount)
        {
            throw new OutOfGroupCapacityException(MaxStudentCount);
        }

        ExtraStudents.Add(extraStudent);
    }
}