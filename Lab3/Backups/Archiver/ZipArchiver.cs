using System.IO.Compression;
using Backups.BackupObjects;
using Backups.Repositories;
using Backups.RepositoryObjects;
using Backups.Storages;
using Backups.Visitors;
using Backups.ZibObjects;

namespace Backups.Archiver;

public class ZipArchiver : IArchiver
{
    public IStorage Archivate(IRepository repository, string? name, int archiveCounter, params BackupObject[] backupObjects)
    {
        using Stream zipToOpen = repository.GetStream(repository.GetPath(), name, archiveCounter);
        using var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create);
        var visitor = new Visitor(archive, repository.GetPath() + name + $"({archiveCounter}).zip");
        foreach (BackupObject backupObject in backupObjects)
        {
            IRepositoryObject repositoryObject = backupObject.GetRepositoryObject();
            repositoryObject.Accept(visitor);
        }

        ZipFolder zipFolder = visitor.GetZipFolderFromList();
        var singleStorage = new SingleStorage(zipFolder, repository, name + $"({archiveCounter}).zip");
        return singleStorage;
    }
}