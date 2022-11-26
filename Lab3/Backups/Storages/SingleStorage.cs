using System.IO.Compression;
using Backups.Repositories;
using Backups.RepositoryObjects;
using Backups.ZibObjects;

namespace Backups.Storages;

public class SingleStorage : IStorage
{
    private ZipFolder _zipFolder;
    private IRepository _repository;
    private string? _name;

    public SingleStorage(ZipFolder zipFolder, IRepository repository, string? name)
    {
        _zipFolder = zipFolder;
        _repository = repository;
        _name = name;
    }

    public List<IRepositoryObject> GetRepositoryObjects()
    {
        var zipObjects = _zipFolder.GetZipObjects();
        using var archive =
            new ZipArchive(
                new FileStream($"{_repository.GetPath()}\\{_name}", FileMode.Open, FileAccess.Read),
                ZipArchiveMode.Read);
        var repositoryObjects = (from zipObject in zipObjects
            let zipArchiveEntry = archive.GetEntry(zipObject.GetName())
            where zipArchiveEntry != null
            select zipObject.GetRepositoryObject(zipArchiveEntry)).ToList();
        return repositoryObjects;
    }

    public int GetStorageCount()
    {
        return 1;
    }
}