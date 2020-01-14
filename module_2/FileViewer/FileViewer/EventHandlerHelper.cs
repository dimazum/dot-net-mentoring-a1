using System;
using DAL.Enums;

namespace FileViewer
{
    public class EventHandlerHelper
    {
        private readonly ConsoleWriter _consoleWriter;
        public EventHandlerHelper(ConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }
        public Operation Operation;

        public Operation FilteredDirectoryFinded(string arg)
        {
            _consoleWriter.ConsoleOutputString($"Filtered directory found : {arg}");
            return Operation;
        }

        public Operation DirectoryFinded(string arg)
        {
            _consoleWriter.ConsoleOutputString($"Directory found : {arg}");
            return Operation;
        }

        public Operation FilteredFileFinded(string arg)
        {
            _consoleWriter.ConsoleOutputString($"Filtered file found : {arg}");
            return Operation;
        }

        public Operation FileFinded(string arg)
        {
            _consoleWriter.ConsoleOutputString($"File found : {arg}");
            return Operation;
        }
    }
}
