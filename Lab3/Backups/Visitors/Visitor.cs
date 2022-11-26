using System.IO.Compression;
using Backups.RepositoryObjects;
using Backups.ZibObjects;
using ZipFile = Backups.ZibObjects.ZipFile;

namespace Backups.Visitors;

public class Visitor : IVisitor
{
    private Stack<ZipArchive> _zipArchives;
    private Stack<List<IZipObject>> _listOfZipObjects;
    private string _path;

    public Visitor(ZipArchive archive, string path)
    {
        _path = path;
        _zipArchives = new Stack<ZipArchive>();
        _listOfZipObjects = new Stack<List<IZipObject>>();
        var listZipObjects = new List<IZipObject>();
        _zipArchives.Push(archive);
        _listOfZipObjects.Push(listZipObjects);
    }

    public void Visit(RepositoryFile repositoryObject)
    {
        ZipArchive zipArchive = _zipArchives.Peek();
        var zipArchiveEntry = zipArchive.CreateEntry(repositoryObject.GetName());
        using Stream streamEntry = repositoryObject.GetFileStream();
        using Stream zipArchiveEntryStream = zipArchiveEntry.Open();
        streamEntry.CopyTo(zipArchiveEntryStream);
        var zipFile = new ZipFile(repositoryObject.Path);
        List<IZipObject> zipObjects = _listOfZipObjects.Peek();
        zipObjects.Add(zipFile);
    }

    public void Visit(RepositoryFolder repositoryObject)
    {
        ZipArchive zipArchive = _zipArchives.Peek();
        var zipArchiveEntry = zipArchive.CreateEntry(repositoryObject.GetName() + ".zip");
        using Stream zipArchiveEntryStream = zipArchiveEntry.Open();
        using var newZipArchive = new ZipArchive(zipArchiveEntryStream, ZipArchiveMode.Create);
        _zipArchives.Push(newZipArchive);
        _listOfZipObjects.Push(new List<IZipObject>());
        var pathObjects = repositoryObject.GetDirectoryStream();
        foreach (IRepositoryObject repoObject in pathObjects)
        {
            repoObject.Accept(this);
        }

        List<IZipObject> topZipObjects = _listOfZipObjects.Pop();
        var zipFolder = new ZipFolder(repositoryObject.Path, topZipObjects);
        _listOfZipObjects.Peek().Add(zipFolder);
        _zipArchives.Pop();
    }

    public ZipFolder GetZipFolderFromList()
    {
        ZipFolder zipFolder = new ZipFolder(_path, _listOfZipObjects.Peek());
        return zipFolder;
    }
}