using IsuExtra.Exceptions;

namespace Isu.Extra.Entities;

public class ExtraClass
{
    private static int _streamCount = 1;

    public ExtraClass(string name)
    {
        Name = name;
        Streams = new List<Stream>();
    }

    public List<Stream> Streams { get; set; }
    public string Name { get; }

    public Stream AddStream(Schedule schedule, int maxStudentCount)
    {
        var newStream = new Stream(_streamCount, maxStudentCount, schedule);
        if (Streams.Any(t => newStream.StreamSchedule.Equals(t.StreamSchedule)))
        {
            throw new StreamsCollisionException();
        }

        _streamCount++;
        Streams.Add(newStream);
        return newStream;
    }

    public void AddStudent(ExtraStudent extraStudent, Stream stream)
    {
        foreach (Stream t in Streams.Where(t => t.Equals(stream)))
        {
            stream.AddExtraStudent(extraStudent);
        }
    }
}