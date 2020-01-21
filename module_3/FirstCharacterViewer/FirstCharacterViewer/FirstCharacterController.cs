using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using FirstCharacterViewer.Interfaces;
using TypeConverter;
using TypeConverter.Interfaces;

namespace FirstCharacterViewer
{
    class FirstCharacterController :IFirstCharacterController
    {

        private readonly IFirstCharacter _firstCharacter;
        private readonly IStringConverter _stringConverter;

        public FirstCharacterController(IFirstCharacter firstCharacter, IStringConverter stringConverter)
        {
            _firstCharacter = firstCharacter;
            _stringConverter = stringConverter;
        }

        public string GetLine()
        {
            Console.WriteLine("введите строку");
            return Console.ReadLine();
        }

        public void GetFirstCharacter()
        {
            while (true)
            {
                _firstCharacter.RegisterMessageHandler(x=>Console.WriteLine(x));
                var str = _firstCharacter.GetFirstCharacter(GetLine());

                if (str == "0")
                {
                    Console.WriteLine("Exit");
                    break;
                }
                Console.WriteLine(str);
            }
        }

        public void ConvertStringToInt()
        {
            while (true)
            {
                int? number = null;
                var str = GetLine();
                try
                {
                    number = _stringConverter.StrToInt(str);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine($"An exception occured: {e}");
                }

                catch (ArgumentException e)
                {
                    Console.WriteLine($"An exception occured: {e}");
                }

                if (str == "e")
                {
                    Console.WriteLine("Exit");
                    break;
                }
                Console.WriteLine($"Number is {number}");
            }
        }
    }
}

