using UnityEngine;

namespace CatchItemsGame
{
    public interface IUIRoot
    {
        Transform Container { get; }
        Transform PoolContainer { get; }
    }
}