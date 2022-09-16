using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTests
{
    private IsuService _myIsu = new IsuService();

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        Group m3102 = _myIsu.AddGroup(new GroupName("M3102"));
        Student student = _myIsu.AddStudent(m3102, "mommy");
        Assert.True(student.Group.Equals(m3102));
        Assert.Contains(student, _myIsu.FindStudents(m3102.GroupName));
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        Group m3102 = _myIsu.AddGroup(new GroupName("M3102"));
        for (int i = 0; i < Group.MaxStudentsCount; ++i)
        {
            Student student = _myIsu.AddStudent(m3102, $"student{i}");
        }

        Assert.Throws<OutOfGroupRangeException>(() => _myIsu.AddStudent(m3102, "aslan"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<InvalidGroupNameException>(() => _myIsu.AddGroup(new GroupName("MMM")));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Assert.Throws<WrongGroupTransferToCurrentGroupException>(() =>
        {
            Group m3102 = _myIsu.AddGroup(new GroupName("M3102"));
            Student aslan = _myIsu.AddStudent(m3102, "aslan");
            _myIsu.ChangeStudentGroup(aslan, m3102);
        });
        Group m3110 = _myIsu.AddGroup(new GroupName("M3110"));
        Group m3113 = _myIsu.AddGroup(new GroupName("M3113"));
        Student aslan = _myIsu.AddStudent(m3110, "aslan");
        _myIsu.ChangeStudentGroup(aslan, m3113);
        Assert.True(aslan.Group.GroupName.Equals(m3113.GroupName));
    }
}