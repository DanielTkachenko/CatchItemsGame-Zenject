using System;

namespace CatchItemsGame
{
    public interface IBootstrap
    {
        event Action OnExecuteAllComandsNotify;
        void Add(ICommand command);
        void Execute();
    }
}