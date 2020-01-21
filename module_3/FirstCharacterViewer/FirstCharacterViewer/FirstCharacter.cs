using System;
using System.Collections.Generic;
using System.Text;
using FirstCharacterViewer.Interfaces;

namespace FirstCharacterViewer
{
    public class FirstCharacter : IFirstCharacter
    {
        private Action<string> _sendMessageHandler;

        public void RegisterMessageHandler(Action<string> sendMessageHandler)
        {
            _sendMessageHandler = sendMessageHandler;
        }
        public string GetFirstCharacter( string line)
        {
  
            string str = string.Empty;
            try
            {
                str = line.Substring(0, 1);
            }
            catch (ArgumentOutOfRangeException exception )
            {
                _sendMessageHandler?.Invoke($"You entered empty string. {exception}");
            }

            return str;
        }
    }
}
