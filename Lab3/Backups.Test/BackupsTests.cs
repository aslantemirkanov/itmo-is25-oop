using Backups.Algorithms;
using Backups.Archiver;
using Backups.BackupObjects;
using Backups.BackupTasks;
using Backups.Repositories;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupsTests
{
    [Fact]
    public void SplitStorageAlgorithmTest()
    {/*
        var fs = new MemoryFileSystem();
        fs.CreateDirectory(@"\from");
        fs.CreateDirectory(@"\to");
        fs.CreateDirectory(@"\from\kek");
        fs.WriteAllText(@"\from\kek\2.txt", "My name is Aslan!");
        fs.WriteAllText(@"\from\1.txt", "Hello!");
        var from = new InMemoryRepository(@"\from", fs);
        var to = new InMemoryRepository(@"\to", fs);
        var obj1 = new BackupObject(@"\1.txt", from);
        var obj2 = new BackupObject(@"\kek\2.txt", from);
        var algo = new SplitStorageAlgorithm();
        var archiver = new ZipArchiver();
        var backupTask = new BackupTask(to);
        backupTask.AddBackupObject(obj1);
        backupTask.AddBackupObject(obj2);
        backupTask.RunStorageAlgorithm(algo, archiver);
        backupTask.RemoveBackupObject(obj2);
        backupTask.RunStorageAlgorithm(algo, archiver);
        int storageCount = 3;
        int restorePointCount = 2;
        fs.OpenFile(@"/to/from/1.txt(1).zip", FileMode.Open, FileAccess.Read);
        Assert.Equal(backupTask.GetStorageCount(), storageCount);
        Assert.Equal(backupTask.GetRestorePointCount(), restorePointCount);*/
    }
}