using Backups.Visitors;

namespace Backups.RepositoryObjects;

public class RepositoryFolder : IFolder
{
    private readonly Func<string, IReadOnlyList<IRepositoryObject>> _streamFromDirectoryFunc;

    public RepositoryFolder(string path, Func<string, IReadOnlyList<IRepositoryObject>> streamFromDirectoryFunc)
    {
        Path = path;
        _streamFromDirectoryFunc = streamFromDirectoryFunc;
    }

    public string Path { get; }

    public string GetName()
    {
        return System.IO.Path.GetFileName(Path);
    }

    public IReadOnlyList<IRepositoryObject> GetDirectoryStream()
    {
        return _streamFromDirectoryFunc.Invoke(Path);
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}