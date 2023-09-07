using System;
using System.Buffers;
using UnityEngine;

namespace CatchItemsGame
{
    public class FallObjectView : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public float FallSpeed => _fallSpeed;
        public FallObjectType ObjectType => _objectType;
        
        public event Action<Collision2D> OnCollisionEnter2DNotify; 
        
        [SerializeField] private SpriteRenderer spriteRenderer;

        private float _fallSpeed;
        private FallObjectType _objectType;

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollisionEnter2DNotify?.Invoke(other);
        }

        void Reset(FallObjectModel model)
        {
            spriteRenderer.sprite = model.ObjectSprite;
            _fallSpeed = model.FallSpeed;
            _objectType = model.Type;
        }

        public class Pool : Zenject.MemoryPool<FallObjectModel, FallObjectView>
        {
            protected override void Reinitialize(FallObjectModel model, FallObjectView item)
            {
                item.Reset(model);
            }
        }
    }
}