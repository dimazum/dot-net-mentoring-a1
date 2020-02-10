using System;
using System.IO;

namespace FileWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
           FileSystemWatcher fileSystemWatcher = new FileSystemWatcher(@"C:\Users\Dzmitry_Khileuski\Desktop\temp\1111");

            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            //fileSystemWatcher.
        }

        private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
