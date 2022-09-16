using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private readonly List<Student> _students;

    public Group(GroupName name)
    {
        _students = new List<Student>();
        GroupName = name;
    }

    public static int MaxStudentsCount => 30;

    public GroupName GroupName { get; }

    public void AddStudent(Student student)
    {
        if (_students.Count >= MaxStudentsCount)
        {
            throw new OutOfGroupRangeException(_students.Count, MaxStudentsCount);
        }

        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        bool isStudentRemoved = _students.Remove(student);
        if (!isStudentRemoved)
        {
            throw new NoSuchStudentException();
        }
    }

    public Student? FindStudentInGroup(StudentId id)
    {
        return _students.FirstOrDefault(student => student.Id == id);
    }

    public IReadOnlyList<Student> GetStudentList()
    {
        return _students;
    }
}