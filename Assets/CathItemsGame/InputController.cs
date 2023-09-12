using System;
using UnityEngine;
using Zenject;


namespace CatchItemsGame
{
    public class InputController : ITickable
    {
        public event Action OnLeftEvent;
        public event Action OnRightEvent;
        
        public void Tick()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                OnLeftEvent?.Invoke();
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                OnRightEvent?.Invoke();
            }
        }
    }
}