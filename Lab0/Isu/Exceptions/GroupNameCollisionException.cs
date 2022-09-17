using Isu.Models;

namespace Isu.Exceptions;

public class GroupNameCollisionException : IsuException
{
    public GroupNameCollisionException(GroupName groupName)
        : base($"Group {groupName} already exist")
    {
    }
}