using Backups.BackupObjects;
using Backups.RestorePoints;

namespace Backups.BackupTasks;

public interface IBackupTask
{
    void AddBackupObject(BackupObject backupObject);
    void RemoveBackupObject(BackupObject backupObject);
}