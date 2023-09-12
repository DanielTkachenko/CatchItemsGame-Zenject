using System;
using System.Buffers;
using UnityEngine;
using Zenject;

namespace CatchItemsGame
{
    public class FallObjectView : MonoBehaviour
    {
        public event Action<FallObjectView, Collision2D> OnCollisionEnter2DNotify;
        
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public FallObjectType ObjectType => _objectType;
        public float FallSpeed => _fallSpeed;
        public bool isCatched;
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        private float _fallSpeed;
        private FallObjectType _objectType;

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollisionEnter2DNotify?.Invoke(this, other);
        }

        public class Pool : Zenject.MemoryPool<Vector3, FallObjectModel, FallObjectView>
        {
            protected override void Reinitialize(Vector3 position, FallObjectModel model, FallObjectView item)
            {
                item.spriteRenderer.sprite = model.ObjectSprite;
                item._fallSpeed = model.FallSpeed;
                item._objectType = model.Type;
                item.transform.localScale = model.DefaultScale;
                item.transform.position = position;
                
            }

            protected override void OnDespawned(FallObjectView item)
            {
                item.gameObject.SetActive(false);
                item.isCatched = false;
            }

            protected override void OnSpawned(FallObjectView item)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}