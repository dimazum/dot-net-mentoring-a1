using System;
using System.IO;
using System.Runtime.CompilerServices;
using BLL.Implementation;
using BLL.Interfaces;
using DAL;
using DAL.Enums;
using FileViewer.Initialization;
using Microsoft.Extensions.Configuration;

namespace FileViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new Controller(new ConsoleWriter());
            var config = new AppSettings().InitializationConfig();
            var fileRepository = new FileRepository(config);
            var consoleWriter = new ConsoleWriter();
            var filter = controller.GetFilter();
            IFileSystemVisitor fileSystemVisitor = new FileSystemVisitor(x => x.Contains(filter ?? string.Empty), fileRepository, SelectType.FilesAndDirectories);
            var eventHendlerHelper = new EventHandlerHelper(new ConsoleWriter())
            {
               Operation = controller.GetOperation()
            };
            fileSystemVisitor.FilesProcessingStart += () => consoleWriter.ConsoleOutputString($"Start processing" + Environment.NewLine+
                                                                                              $"--------------------");
            fileSystemVisitor.FilesProcessingFinish += () => consoleWriter.ConsoleOutputString($"--------------------" + Environment.NewLine +
                                                                                               $"Finish processing");
            fileRepository.FileFinded += eventHendlerHelper.FileFinded;
            fileRepository.FilteredFileFinded += eventHendlerHelper.FilteredFileFinded;
            fileRepository.DirectoryFinded += eventHendlerHelper.DirectoryFinded;
            fileRepository.FilteredDirectoryFinded += eventHendlerHelper.FilteredDirectoryFinded;

            consoleWriter.ConsoleOutputEnumerable(fileSystemVisitor);

            Console.ReadLine();
        }
    }
}
