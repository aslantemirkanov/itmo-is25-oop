namespace Isu.Exceptions;

public class InvalidGroupNameException : IsuException
{
    public InvalidGroupNameException(string name)
        : base($"Group with name {name} cannot exist")
    {
    }
}