using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CatchItemsGame
{
    public class FallObjectController
    {
        public static event Action<float> DamageToPlayerNotify;
        public static event Action<int> ScoresToPlayerNotify;
        
        private FallObjectAnimator _animator;
        private FallObjectConfig _fallObjectConfig;
        private List<FallObjectView> _views;
        private FallObjectView.Pool _objectPool;
        
        private Vector3 _deltaVector = new Vector3(0, -0.001f, 0);
        private float _minPositionY = -7f;

        private bool isActive;


        public FallObjectController(
            FallObjectView.Pool objectPool,
            FallObjectConfig fallObjectConfig
        )
        {
            TickableManager.FixedUpdateNotify += Tick;
            _fallObjectConfig = fallObjectConfig;
            _objectPool = objectPool;
            
            _views = new List<FallObjectView>();
            _animator = new FallObjectAnimator(this);
            _animator.DeathAnimationEnded += Destroy;
        }

        public FallObjectView Create(Vector3 position, FallObjectType type)
        {
            var model = _fallObjectConfig.Get(type);
            var view = _objectPool.Spawn(position, model);

            _views.Add(view);
            view.OnCollisionEnter2DNotify += OnCollisionEnter2D;
            
            _animator.Spawn(view);

            return view;
        }

        public void StartGame()
        {
            isActive = true;
        }

        public void StopGame()
        {
            isActive = false;
            DestroyAll();
        }

        private void DestroyAll()
        {
            foreach (var view in _views)
            {
                if (view.gameObject.activeInHierarchy)
                {
                    _objectPool.Despawn(view);
                }
            }
            
            _views.Clear();
        }

        public void Destroy(FallObjectView view)
        {
            _objectPool.Despawn(view);
            _views.Remove(view);
        }

        void OnCollisionEnter2D(FallObjectView view, Collision2D collision2D)
        {
            var player = collision2D.gameObject.GetComponent<PlayerView>();

            if (player != null)
            {
                _animator.Death(view);
                var points = _fallObjectConfig.Get(view.ObjectType).PointsPerObject;
                ScoresToPlayerNotify?.Invoke(points);
            }
        }
        
        public void Tick()
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