using Backups.BackupObjects;
using Backups.Repositories;
using Backups.Storages;

namespace Backups.Archiver;

public interface IArchiver
{
    IStorage Archivate(IRepository repository, string? name, int archiveCounter, params BackupObject[] backupObjects);
}