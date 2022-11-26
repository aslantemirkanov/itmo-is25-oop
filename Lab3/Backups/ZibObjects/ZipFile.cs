using System.IO.Compression;
using Backups.RepositoryObjects;

namespace Backups.ZibObjects;

public class ZipFile : IZipObject
{
    public ZipFile(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public string GetName()
    {
        string[] parts = Path.Split('\\');
        return parts.Last();
    }

    public IRepositoryObject GetRepositoryObject(ZipArchiveEntry zipArchiveEntry)
    {
        return new RepositoryFile(Path, s => zipArchiveEntry.Open());
    }
}