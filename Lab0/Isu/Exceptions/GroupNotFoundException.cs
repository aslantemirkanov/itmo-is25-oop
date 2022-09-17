using Isu.Models;

namespace Isu.Exceptions;

public class GroupNotFoundException : IsuException
{
    public GroupNotFoundException(GroupName groupName)
        : base($"Group {groupName} doesn't exist")
    {
    }
}