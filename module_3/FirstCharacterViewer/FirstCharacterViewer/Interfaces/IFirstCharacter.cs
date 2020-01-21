using System;

namespace FirstCharacterViewer.Interfaces
{
    public interface IFirstCharacter
    {
        string GetFirstCharacter(string line);
        void RegisterMessageHandler(Action<string> sendMessageHandler);
    }
}