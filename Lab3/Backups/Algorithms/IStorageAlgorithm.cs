using Backups.Archiver;
using Backups.BackupObjects;
using Backups.Repositories;
using Backups.Storages;

namespace Backups.Algorithms;

public interface IStorageAlgorithm
{
    IStorage RunAlgorithm(IRepository repository, IArchiver archiver, int archiveCounter, params BackupObject[] backupObjects);
}