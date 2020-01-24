using System;
using System.Collections.Generic;
using System.Text;
using FirstCharacterViewer.Interfaces;

namespace FirstCharacterViewer
{
    public class FirstCharacter : IFirstCharacter
    {
        private Action<string> _sendMessage;
        private Action<string> _recoveryStateAfterException;
        private bool _isException;

        public void RegisterSendMessage(Action<string> sendMessageHandler)
        {
            _sendMessage = sendMessageHandler;
        }
        public void RegisterRecoveryStateAfterException(Action<string> recoveryStateAfterException)
        {
            _recoveryStateAfterException = recoveryStateAfterException;
        }
        public string GetFirstCharacter( string line)
        {
            if (line == null)
            {
                return string.Empty;
            }

            string str = string.Empty;
            try
            {
                str = line.Substring(0, 1);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                _sendMessage?.Invoke($"You entered empty string. {exception}");
                _isException = true;
            }
            finally
            {
                if (_isException)
                {
                    _isException = false;
                    _recoveryStateAfterException?.Invoke("Do you want to try again? Y - yes, N - no");
                }
            }
            
            return str;
        }
    }
}
