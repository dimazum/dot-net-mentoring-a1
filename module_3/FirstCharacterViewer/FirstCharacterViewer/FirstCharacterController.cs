using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using FirstCharacterViewer.Interfaces;
using TypeConverter;
using TypeConverter.Interfaces;

namespace FirstCharacterViewer
{
    class FirstCharacterController : IFirstCharacterController
    {
        private readonly IFirstCharacter _firstCharacter;
        private readonly IStringConverter _stringConverter;

        public FirstCharacterController(IFirstCharacter firstCharacter, IStringConverter stringConverter)
        {
            _firstCharacter = firstCharacter;
            _stringConverter = stringConverter;
        }

        private string GetLine()
        {
            Console.WriteLine("Enter string");
            return Console.ReadLine();
        }

        public void GetFirstCharacter()
        {
            _firstCharacter.RegisterRecoveryStateAfterException(RecoveryStateAfterExceptionHandler);
            _firstCharacter.RegisterSendMessage(x => Console.WriteLine(x));
            var str = _firstCharacter.GetFirstCharacter(GetLine());

            if (!string.IsNullOrEmpty(str))
            {
                Console.WriteLine($"First character is {str}");
            }
        }

        public void ConvertStringToInt()
        {
            var isException = false;
            int? number = null;

            try
            {
                number = _stringConverter.StrToInt(GetLine());
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine($"An exception occured: {e}");
                isException = true;
            }

            catch (ArgumentException e)
            {
                isException = true;
                Console.WriteLine($"An exception occured: {e}");
            }

            finally
            {
                if (isException)
                {
                    RecoveryStateAfterException("Do you want to try again? Y - yes, N - no");
                }
            }

            if (!isException)
            {
                Console.WriteLine($"Number is {number}");
            }
        }

        private void RecoveryStateAfterExceptionHandler(string str)
        {
            Console.WriteLine(str);
            var line = Console.ReadLine()?.ToLower();
            if (line == "y")
            {
                GetFirstCharacter();
            }
        }
        private void RecoveryStateAfterException(string str)
        {
            Console.WriteLine(str);
            var line = Console.ReadLine()?.ToLower();
            if (line == "y")
            {
                ConvertStringToInt();
            }
        }
    }
}

