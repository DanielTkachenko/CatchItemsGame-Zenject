using UnityEngine;

namespace CatchItemsGame
{
    public class PlayerView : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        
        [SerializeField] private SpriteRenderer spriteRenderer;
    }
}