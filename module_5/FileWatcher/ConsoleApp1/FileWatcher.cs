using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp1.Configuration;
using ConsoleApp1.Resources;

namespace ConsoleApp1
{
    public class FileWatcher
    {
        private int _count;
        private readonly WatcherConfigurationSection _config;

        public FileWatcher(WatcherConfigurationSection config)
        {
            _config = config;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(_config.CultureInfo.Culture);
        }

        public void StartWatching()
        {
            var fileSystemWatcherList = FileSystemWatcherInit(_config.Folders);

            var isMoved = false;

            Console.WriteLine(Logs.StartWatching);
            Console.WriteLine(Logs.Exit);

            foreach (var fileSystemWatcher in fileSystemWatcherList)
            {
                fileSystemWatcher.EnableRaisingEvents = true;
                fileSystemWatcher.Created += (sender, e) =>
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(_config.CultureInfo.Culture);//??

                    Console.WriteLine(Logs.NewFile, e.Name);
                    foreach (var rule in _config.Rules)
                    {
                        var _rule = rule as RuleElement;
                        if (e.Name.Contains(_rule?.Name ?? throw new InvalidOperationException()))
                        {
                            Console.WriteLine(Logs.AppropriateRule, _rule.Name);
                            var prefix = AddPrefixToName(_rule.IsAddDate,
                                _rule.IsAddNumber);

                            MoveWithReplace(e.FullPath,$"{ _rule.Folder}/{prefix + e.Name}");
 
                            isMoved = true;
                            break;
                        }

                        MoveWithReplace(e.FullPath,
                            _config.DefaultFolder.Path + e.Name);
                        isMoved = true;
                        break;
                    }

                    Console.WriteLine(Logs.Exit);
                };

                if (isMoved)
                {
                    break;
                }
            }

            Console.ReadLine();

            foreach (var fileSystemWatcher in fileSystemWatcherList)
            {
                fileSystemWatcher.Dispose();
            }
        }

        private IEnumerable<FileSystemWatcher> FileSystemWatcherInit( IEnumerable folders)
        {
            var fileSystemWatcherList = new List<FileSystemWatcher>();

            foreach (var folder in folders)
            {
                fileSystemWatcherList.Add(
                    new FileSystemWatcher((folder as FolderElement)?.Path ?? throw new InvalidOperationException()));
            }

            return fileSystemWatcherList;
        }

        private string AddPrefixToName(bool? isAddDate, bool? isAddNumber)
        {
            string prefix = string.Empty;

            if (isAddDate == true)
            {
                prefix = $" {DateTime.Today.ToString(Logs.DatePrefix)}_";
            }

            if (isAddNumber == true)
            {
                prefix += $"N{_count++}_";
            }

            return prefix;
        }

        private void MoveWithReplace(string sourceFileName, string destFileName)
        {
            Thread.Sleep(200);
            if (File.Exists(destFileName))
            {
                File.Delete(destFileName);
            }

            try
            {
                File.Move(sourceFileName, destFileName);
                Console.WriteLine(Logs.FileMoved, destFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(Logs.ErrorFileNotMoved + e.Message);
            }
        }
    }
}
