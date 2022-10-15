using Isu.Models;

namespace IsuExtra.Exceptions;
public class ExtraGroupCollisionException : IsuExtraException
{
    public ExtraGroupCollisionException(GroupName groupName)
        : base($"Group {groupName.ToString()} is already exists")
    {
    }
}