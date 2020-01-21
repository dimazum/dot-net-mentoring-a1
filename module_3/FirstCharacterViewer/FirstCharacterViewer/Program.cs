using System;
using System.Linq;
using System.Threading.Channels;
using TypeConverter;

namespace FirstCharacterViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstCharacterController firstCharacterController = new FirstCharacterController(new FirstCharacter(), new StringConvertor());
            //firstCharacterController.ConvertStringToInt();
            firstCharacterController.GetFirstCharacter();


            Console.ReadLine();
        }
 
    }
}
