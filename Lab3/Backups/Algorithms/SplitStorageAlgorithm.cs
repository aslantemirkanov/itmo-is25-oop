using Backups.Archiver;
using Backups.BackupObjects;
using Backups.Repositories;
using Backups.Storages;

namespace Backups.Algorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    public IStorage RunAlgorithm(IRepository repository, IArchiver archiver, int archiveCounter, params BackupObject[] backupObjects)
    {
        var singleStorages = backupObjects.Select(backupObject =>
            archiver.Archivate(repository, '\\' + backupObject.GetName(), archiveCounter, backupObject)).ToList();
        var splitStorage = new SplitStorage(singleStorages);
        return splitStorage;
    }
}