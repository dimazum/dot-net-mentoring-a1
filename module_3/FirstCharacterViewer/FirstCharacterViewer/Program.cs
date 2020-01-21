using FirstCharacterViewer.Initialization;
using System;
using System.Linq;
using System.Threading.Channels;
using FirstCharacterViewer.Interfaces;
using TypeConverter;

namespace FirstCharacterViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap.Start();

            var firstCharacterController = Bootstrap.container.GetInstance<IFirstCharacterController>();

            //FirstCharacterController firstCharacterController = new FirstCharacterController(new FirstCharacter(), new StringConvertor());
            firstCharacterController.ConvertStringToInt();



            Console.ReadLine();
        }
 
    }
}
