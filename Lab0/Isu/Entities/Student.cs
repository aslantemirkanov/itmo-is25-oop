using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(Group group, string name, StudentId id)
    {
        Name = name;
        Group = group;
        Id = id;
    }

    public StudentId Id { get; }
    public Group Group { get; private set; }
    public string Name { get; }

    public void ChangeGroup(Group newGroup)
    {
        Group = newGroup;
    }
}