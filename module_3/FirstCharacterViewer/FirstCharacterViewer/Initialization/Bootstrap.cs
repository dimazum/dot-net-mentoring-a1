using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;
using FirstCharacterViewer.Interfaces;
using TypeConverter.Interfaces;
using TypeConverter;

namespace FirstCharacterViewer.Initialization
{
    class Bootstrap
    {
        public static Container container;

        public static void Start()
        {
            container = new Container();
            container.Register<IFirstCharacterController, FirstCharacterController>();
            container.Register<IFirstCharacter, FirstCharacter>();
            container.Register<IStringConverter, StringConverter>(Lifestyle.Singleton);
            
            container.Verify();
        }
    }
}
