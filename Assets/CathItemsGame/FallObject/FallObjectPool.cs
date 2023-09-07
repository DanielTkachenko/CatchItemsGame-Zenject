using System.Collections.Generic;
using UnityEngine;

namespace CatchItemsGame
{
    public class FallObjectPool
    {
        private readonly FallObjectFactory _factory;
        private readonly PlayerScoreCounter _playerScoreCounter;

        private Dictionary<FallObjectView, FallObjectController> _pool;

        private GameObject _container; 

        public FallObjectPool(FallObjectFactory factory, PlayerScoreCounter playerScoreCounter)
        {
            _factory = factory;
            _playerScoreCounter = playerScoreCounter;
            _pool = new Dictionary<FallObjectView, FallObjectController>();
            _container = new GameObject("FallObjects");
        }

        public FallObjectView CreateObject(FallObjectType type)
        {
            var freeObject = GetFreeElement(type);

            if (freeObject)
            {
                return freeObject;
            }
            
            var controller = _factory.Create(type);
            var view = controller.View;
            
            controller.SetActive(true);
            view.transform.parent = _container.transform;
            controller.ObjectFellNotify += (FallObjectController) => ReturnToPool(view);
            controller.DeathAnimationEndedNotify += (FallObjectController) => ReturnToPool(view);
            controller.PlayerCatchFallingObjectNotify += _playerScoreCounter.PlayerCatchFallObjectEventHandler;
            
            _pool.Add(view, controller);

            return view;
        }

        private FallObjectView GetFreeElement(FallObjectType type)
        {
            foreach (var fallObjectController in _pool.Values)
            {
                if (!fallObjectController.View.gameObject.activeInHierarchy)
                {
                    fallObjectController.SetModel(_factory.ObjectConfig.Get(type));
                    fallObjectController.SetActive(true);
                    return fallObjectController.View;
                }
            }
            
            return null;
        }

        public void ReturnToPool(FallObjectView fallObject)
        {
            if (_pool.TryGetValue(fallObject, out var controller))
            {
                controller.SetActive(false);
            }
            else
            {
                Debug.Log("There is no such object in pool");
                return;
            }
        }

        public FallObjectController GetController(FallObjectView view)
        {
            return _pool[view];
        }
        public void AllReturnToPool()
        {
            foreach (var fallObject in _pool.Keys)
            {
                if (fallObject.gameObject.activeInHierarchy)
                {
                    ReturnToPool(fallObject);
                }
            }
        }
    }
}
