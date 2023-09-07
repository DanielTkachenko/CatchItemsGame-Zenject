using System;
using System.Buffers;
using UnityEngine;

namespace CatchItemsGame
{
    public class FallObjectView : MonoBehaviour
    {
        public event Action<FallObjectView, Collision2D> OnCollisionEnter2DNotify; 
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public float FallSpeed => _fallSpeed;
        public FallObjectType ObjectType => _objectType;

        [SerializeField] private SpriteRenderer spriteRenderer;

        private Vector3 _defaultScale = new Vector3(0.15f, 0.15f, 0.15f);
        private float _fallSpeed;
        private FallObjectType _objectType;

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollisionEnter2DNotify?.Invoke(this, other);
        }

        void Reset(Vector3 position, FallObjectModel model)
        {
            spriteRenderer.sprite = model.ObjectSprite;
            _fallSpeed = model.FallSpeed;
            _objectType = model.Type;
            transform.localScale = _defaultScale;
            transform.position = position;
        }

        public class Pool : Zenject.MemoryPool<Vector3, FallObjectModel, FallObjectView>
        {
            protected override void Reinitialize(Vector3 position, FallObjectModel model, FallObjectView item)
            {
                item.Reset(position, model);
            }
        }
    }
}