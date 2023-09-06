﻿using UnityEngine;

namespace CatchItemsGame
{
    public class CreateTickableManagerCommand: Command
    {
        private readonly TickableManager _tickableManagerPrefab;
        private TickableManager _tickableManager;
        
        public CreateTickableManagerCommand()
        {
            _tickableManagerPrefab = Resources.Load<TickableManager>(ResourcesConst.TickableManager);
        }
        public override void Execute()
        {
            _tickableManager = Object.Instantiate(_tickableManagerPrefab);
            base.Execute();
        }

        public override void Undo()
        {
            Object.Destroy(_tickableManager);
        }
    }
}