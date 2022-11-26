using Backups.Archiver;
using Backups.BackupObjects;
using Backups.Repositories;
using Backups.RepositoryObjects;
using Backups.Storages;

namespace Backups.Algorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    public IStorage RunAlgorithm(IRepository repository, IArchiver archiver, int archiveCounter, params BackupObject[] backupObjects)
    {
        return archiver.Archivate(repository, @"\Backup", archiveCounter, backupObjects.ToArray());
    }
}