namespace IsuExtra.Exceptions;

public class StreamsCollisionException : IsuExtraException
{
    public StreamsCollisionException()
        : base($"That stream is already exists")
    {
    }
}