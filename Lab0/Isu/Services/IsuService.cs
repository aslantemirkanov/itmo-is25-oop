using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Group> _isuGroups;
    private readonly IdGenerator _idGenerator;

    public IsuService()
    {
        _isuGroups = new List<Group>();
        _idGenerator = new IdGenerator();
    }

    public Group AddGroup(GroupName name)
    {
        if (_isuGroups.Any(group => group.GroupName.Equals(name)))
        {
            throw new GroupNameCollisionException(name);
        }

        var newGroup = new Group(name);
        _isuGroups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        var student = new Student(group, name, _idGenerator.GenerateNewId());
        Group? foundGroup = FindGroup(group.GroupName);
        if (foundGroup == null)
        {
            throw new GroupNotFoundException(group.GroupName);
        }

        foundGroup.AddStudent(student);
        return student;
    }

    public Student GetStudent(StudentId id)
    {
        Student? foundStudent = FindStudent(id);
        if (foundStudent == null)
        {
            throw new NoSuchStudentException();
        }

        return foundStudent;
    }

    public Student? FindStudent(StudentId id)
    {
        return _isuGroups.Select(
            group => group.FindStudentInGroup(id)).FirstOrDefault(foundStudent => foundStudent != null);
    }

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        Group? foundGroup = FindGroup(groupName);
        if (foundGroup != null)
        {
            return foundGroup.GetStudentList();
        }

        throw new GroupNotFoundException(groupName);
    }

    public IReadOnlyList<Student> FindStudents(CourseNumber courseNumber)
    {
        var foundStudentList = _isuGroups
            .Where(group => group.GroupName.CourseNumber.StudentCourse.Equals(courseNumber.StudentCourse))
            .SelectMany(group => group.GetStudentList()).ToList();

        if (!foundStudentList.Any())
        {
            throw new NoStudentsFromThisCourseException(courseNumber);
        }

        return foundStudentList;
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _isuGroups.FirstOrDefault(group => group.GroupName == groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        var foundGroupList = _isuGroups
            .Where(group => group.GroupName.CourseNumber.StudentCourse.Equals(courseNumber.StudentCourse))
            .ToList();

        if (!foundGroupList.Any())
        {
            throw new NoGroupFromThisCourseException(courseNumber);
        }

        return foundGroupList;
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (newGroup.GroupName.Equals(student.Group.GroupName))
        {
            throw new WrongGroupTransferToCurrentGroupException();
        }

        FindGroup(student.Group.GroupName)?.RemoveStudent(student);
        FindGroup(newGroup.GroupName)?.AddStudent(student);
        student.ChangeGroup(newGroup);
    }
}