using System;
using System.Collections.Generic;
using UnityEngine;

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


        public FallObjectController(
            FallObjectView.Pool objectPool
        )
        {
            _views = new List<FallObjectView>();
            _fallObjectConfig = Resources.Load<FallObjectConfig>("FallObjectConfig");
            _objectPool = objectPool;
            
            _animator = new FallObjectAnimator(this);
            _animator.DeathAnimationEnded += Destroy;

            TickableManager.FixedUpdateNotify += FixedUpdate;
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

        public void DestroyAll()
        {
            foreach (var view in _views)
            {
                _objectPool.Despawn(view);
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
            }
        }

        private void FixedUpdate()
        {
            foreach (var view in _views)
            {
                if (view.transform.position.y <= _minPositionY)
                {
                    var damage = _fallObjectConfig.Get(view.ObjectType).Damage;
                    DamageToPlayerNotify?.Invoke(damage);
                }

                view.transform.position += _deltaVector * view.FallSpeed;
            }
            
        }
    }
}