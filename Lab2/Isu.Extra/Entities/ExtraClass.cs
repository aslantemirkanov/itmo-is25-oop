namespace Isu.Extra.Entities;

public class ExtraClass
{
    public ExtraClass(List<Stream> streams)
    {
        Streams = streams;
    }

    public List<Stream> Streams { get; }
}