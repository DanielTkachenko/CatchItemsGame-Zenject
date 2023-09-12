using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CatchItemsGame
{
    public class FallObjectController : IFixedTickable
    {
        public static event Action<float> DamageToPlayerNotify;
        public static event Action<int> ScoresToPlayerNotify;
        
        private FallObjectAnimator _animator;
        private FallObjectConfig _fallObjectConfig;
        private readonly TickableManager _tickableManager;
        private List<FallObjectView> _views;
        private FallObjectView.Pool _objectPool;
        
        private Vector3 _deltaVector = new Vector3(0, -0.001f, 0);
        private float _minPositionY = -5f;


        public FallObjectController(
            FallObjectView.Pool objectPool,
            FallObjectConfig fallObjectConfig,
            TickableManager tickableManager
        )
        {
            _fallObjectConfig = fallObjectConfig;
            _tickableManager = tickableManager;
            _objectPool = objectPool;
            
            _views = new List<FallObjectView>();
            _animator = new FallObjectAnimator(this);
            _animator.DeathAnimationEnded += (view) =>
            {
                _objectPool.Despawn(view);
                _views.Remove(view);
            };
        }

        public FallObjectView Create(Vector3 position, FallObjectType type)
        {
            var model = _fallObjectConfig.Get(type);
            var view = _objectPool.Spawn(position, model);

            _views.Add(view);
            view.OnCollisionEnter2DNotify += OnCollisionEnter2D;
            
            _animator.SpawnAnimation(view);

            return view;
        }

        public void StartGame()
        {
            _tickableManager.AddFixed(this);
        }

        public void StopGame()
        {
            _tickableManager.RemoveFixed(this);
            foreach (var view in _views)
            {
                if (view.gameObject.activeInHierarchy)
                {
                    _objectPool.Despawn(view);
                }
            }
            
            _views.Clear();
        }

        void OnCollisionEnter2D(FallObjectView view, Collision2D collision2D)
        {
            if (!view.isCatched)
            {
                var player = collision2D.gameObject.GetComponent<PlayerView>();
                
                if (player != null)
                {
                    _animator.DeathAnimation(view);
                    var points = _fallObjectConfig.Get(view.ObjectType).PointsPerObject;
                    ScoresToPlayerNotify?.Invoke(points);
                    view.isCatched = true;
                }
            }
        }
        
        public void FixedTick()
        {
            for (int i = 0; i < _views.Count; i++)
            {
                var view = _views[i];
                view.transform.position += _deltaVector * view.FallSpeed;
                if (view.transform.position.y <= _minPositionY)
                {
                    var damage = _fallObjectConfig.Get(view.ObjectType).Damage;
                    DamageToPlayerNotify?.Invoke(damage);
                    _objectPool.Despawn(view);
                    _views.Remove(view);
                }
            }
        }
    }
}