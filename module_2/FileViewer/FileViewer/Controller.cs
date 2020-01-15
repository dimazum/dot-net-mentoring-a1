using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileViewer
{
    public class Controller
    {
        private readonly ConsoleWriter _consoleWriter;
        public Controller(ConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }
        public string GetFilter()
        {
            _consoleWriter.ConsoleOutputString("Type string for filtering...");
            string filter = Console.ReadLine();

            return filter;
        }

        public Operation GetOperation()
        {
            _consoleWriter.ConsoleOutputString("Enter 1 - to exclude files...2 - to exclude directories... 3 - to find item and stop...enter to continue");
            int.TryParse(Console.ReadLine(), out var number);


            var operation = Operation.ContinueSearch;
            switch (number)
            {
                case 1:
                    operation = Operation.ExcludeFiles;
                    break;
                case 2:
                    operation = Operation.ExcludeDirectories;
                    break;
                case 3:
                    operation = Operation.Stop;
                    break;
            }

            return operation;
        }
    }
}
