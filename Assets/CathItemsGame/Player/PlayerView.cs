using UnityEngine;
using Zenject;

namespace CatchItemsGame
{
    public class PlayerView : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        
        [SerializeField] private SpriteRenderer spriteRenderer;

        public class Factory : PlaceholderFactory<PlayerView>
        {
        }
    }
}