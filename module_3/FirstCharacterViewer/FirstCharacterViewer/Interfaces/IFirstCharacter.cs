using System;

namespace FirstCharacterViewer.Interfaces
{
    public interface IFirstCharacter
    {
        string GetFirstCharacter(string line);
        void RegisterSendMessage(Action<string> sendMessageHandler);
        void RegisterRecoveryStateAfterException(Action<string> recoveryStateAfterException);
    }
}