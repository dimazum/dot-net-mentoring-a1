using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Implementation
{
    public class FileSystemVisitor : IFileSystemVisitor 
    {
        private readonly Func<string, bool> _filter;
        private readonly IFileRepository _fileRepository;
        private readonly SelectType _selectType;
        public event Action FilesProcessingStart;
        public event Action FilesProcessingFinish;


        public FileSystemVisitor(IFileRepository fileRepository, SelectType selectType)
        {
            _fileRepository = fileRepository;
            _selectType = selectType;
        }

        public FileSystemVisitor(Func<string, bool> filter, IFileRepository fileRepository, SelectType selectType)
        {
            _filter = filter;
            _fileRepository = fileRepository;
            _selectType = selectType;
        }

        public IEnumerator<string> GetEnumerator()
        {
            FilesProcessingStart?.Invoke();
            if (_selectType == SelectType.Files)
            {
                foreach (var file in _fileRepository.GetFiles(_filter))
                {
                    yield return file;
                }
            }
            else if (_selectType == SelectType.Directories)
            {
                foreach (var file in _fileRepository.GetDirectories(_filter))
                {
                    yield return file;
                }
            }
            else if (_selectType == SelectType.FilesAndDirectories)
            {
                foreach (var file in _fileRepository.GetDirectories(_filter).Concat(_fileRepository.GetFiles(_filter)))
                {
                    yield return file;
                }
            }
            FilesProcessingFinish?.Invoke();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
