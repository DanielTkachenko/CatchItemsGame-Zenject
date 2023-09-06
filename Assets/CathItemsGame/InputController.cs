using System;
using UnityEngine;


namespace CatchItemsGame
{
    public class InputController
    {
        public event Action OnLeftEvent;
        public event Action OnRightEvent;

        public InputController()
        {
            TickableManager.UpdateNotify += CheckInput;
        }

        private void CheckInput()
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