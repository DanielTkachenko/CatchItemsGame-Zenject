using System;

namespace CatchItemsGame
{
    public interface ICommand
    {
        event Action OnCommandExecuteNotify;
        void Execute();
        void Undo();
    }
}