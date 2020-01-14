using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using System.Configuration;
using System.Threading;
using DAL.Enums;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class FileRepository : IFileRepository
    {
        private readonly string _path;
        private Operation? _operation;

        public event Func<string, Operation> FileFinded;
        public event Func<string, Operation> DirectoryFinded;
        public event Func<string, Operation> FilteredFileFinded;
        public event Func<string, Operation> FilteredDirectoryFinded;

        public FileRepository(IConfiguration configuration)
        {
            _path = configuration["rootDirectoryPath"];
        }

        public IEnumerable<string> GetFiles(Func<string, bool> filter)
        {
            return this.GetFiles(_path, filter);
        }
        private IEnumerable<string> GetFiles(string path, Func<string, bool> filter)
        {
            var files = new List<string>();

            if (_operation == Operation.Stop)
                return files;

            var rootFiles = Directory.GetFiles(path);

            foreach (var file in rootFiles)
            {
                var fileInfo = new FileInfo(file);

                if ((_operation = FileFinded?.Invoke(fileInfo.Name)) == Operation.Stop)
                    return files;

                if (filter == null)
                {
                    files.Add(file);
                }
                else if (filter.Invoke(fileInfo.Name))
                {
                    _operation = FilteredFileFinded?.Invoke(fileInfo.Name);
                    if (_operation != Operation.ExcludeFiles)
                    {
                        files.Add(file);
                    }
                }
            }

            foreach (var dir in Directory.GetDirectories(path))
            {
                files.AddRange(GetFiles(dir, filter));
            }
            return files;
        }

        public IEnumerable<string> GetDirectories(Func<string, bool> filter)
        {
            return this.GetDirectories(_path, filter);
        }

        private IEnumerable<string> GetDirectories(string path, Func<string, bool> filter)
        {
            var directories = new List<string>();
            if (_operation == Operation.Stop)
                return directories;

            var rootDirectories = Directory.GetDirectories(path);

            foreach (var dir in rootDirectories)
            {
                var directoryInfo = new DirectoryInfo(dir);

                if ((_operation = DirectoryFinded?.Invoke(directoryInfo.Name)) == Operation.Stop)
                    return directories;

                if (filter == null)
                {
                    if (_operation != Operation.ExcludeDirectories)
                    {
                        directories.Add(dir);
                    }
                }
                else if (filter.Invoke(directoryInfo.Name))
                {
                    _operation = FilteredDirectoryFinded?.Invoke(directoryInfo.Name);
                    if (_operation != Operation.ExcludeDirectories)
                    {
                        directories.Add(dir);
                    }
                }
                directories.AddRange(GetDirectories(dir, filter));
            }
            return directories;
        }

    }
}

