using System.IO.Compression;
using Backups.RepositoryObjects;

namespace Backups.ZibObjects;

public class ZipFolder : IZipObject
{
    private List<IZipObject> _zipObjects;
    private Func<IReadOnlyList<ZipArchiveEntry>, IReadOnlyList<IRepositoryObject>> _streamFromDirectoryFunc;

    public ZipFolder(string path, List<IZipObject> zipObjects)
    {
        _zipObjects = zipObjects;
        Path = path;
        _streamFromDirectoryFunc = GetListRepositoryObjects;
    }

    public string Path { get; }

    public IRepositoryObject GetRepositoryObject(ZipArchiveEntry zipArchiveEntry)
    {
        using var archive = new ZipArchive(zipArchiveEntry.Open(), ZipArchiveMode.Read);
        IReadOnlyList<ZipArchiveEntry> zipArchiveEntries = archive.Entries;
        return new RepositoryFolder(Path, s => GetListRepositoryObjects(zipArchiveEntries));
    }

    public IReadOnlyList<IZipObject> GetZipObjects()
    {
        return _zipObjects;
    }

    public string GetName()
    {
        return System.IO.Path.GetFileName(Path);
    }

    private IReadOnlyList<IRepositoryObject> GetListRepositoryObjects(IReadOnlyList<ZipArchiveEntry> zipArchiveEntries)
    {
        return (from zipObject in _zipObjects
            from iZipArchiveEntry in zipArchiveEntries
            where iZipArchiveEntry.Name.Equals(zipObject.GetName())
            select GetRepositoryObject(iZipArchiveEntry)).ToList();
    }
}