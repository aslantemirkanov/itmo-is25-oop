using Backups.Visitors;

namespace Backups.RepositoryObjects;

public class RepositoryFile : IFile
{
    private readonly Func<string, Stream> _streamFromFileFunc;

    public RepositoryFile(string path, Func<string, Stream> streamFromFileFunc)
    {
        Path = path;
        _streamFromFileFunc = streamFromFileFunc;
    }

    public string Path { get; }

    public Stream GetFileStream()
    {
        return _streamFromFileFunc.Invoke(Path);
    }

    public string GetName()
    {
        return System.IO.Path.GetFileName(Path);
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}