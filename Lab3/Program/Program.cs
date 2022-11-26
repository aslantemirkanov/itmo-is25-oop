using Backups.Algorithms;
using Backups.Archiver;
using Backups.BackupObjects;
using Backups.BackupTasks;
using Backups.Repositories;

namespace Program;

public static class Program
{
    public static void Main(string[] args)
    {
        
        var from = new FileSystemRepository(@"C:\Users\Aslan\Desktop\ITMO\rp\from");
        var to1 = new FileSystemRepository(@"C:\Users\Aslan\Desktop\ITMO\rp\to\Single");
        var to2 = new FileSystemRepository(@"C:\Users\Aslan\Desktop\ITMO\rp\to\Split");
        var backupTask1 = new BackupTask(to1);
        var backupTask2 = new BackupTask(to2);
        var obj1 = new BackupObject(@"\kek", from);
        var obj2 = new BackupObject(@"\1.txt", from);
        var obj3 = new BackupObject(@"\kek\aboba\ahah.xlsx", from);
        var obj4 = new BackupObject(@"\kek\2.txt", from);
        var archiver = new ZipArchiver();
        var algorithm = new SingleStorageAlgorithm();
        backupTask1.AddBackupObject(obj1);
        backupTask1.AddBackupObject(obj2);
        backupTask1.AddBackupObject(obj3);
        backupTask1.AddBackupObject(obj4);
        backupTask1.RunStorageAlgorithm(algorithm, archiver);
        backupTask1.RunStorageAlgorithm(algorithm, archiver);
        backupTask1.RunStorageAlgorithm(algorithm, archiver);
        var algorithm2 = new SplitStorageAlgorithm();
        backupTask2.AddBackupObject(obj1);
        backupTask2.AddBackupObject(obj2);
        backupTask2.AddBackupObject(obj4);
        backupTask2.RunStorageAlgorithm(algorithm2, archiver);
        backupTask2.RemoveBackupObject(obj1);
        backupTask2.RunStorageAlgorithm(algorithm2, archiver);
    }
}