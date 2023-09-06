using UnityEngine;

namespace CatchItemsGame
{
    public class CameraView : MonoBehaviour
    {
        public UnityEngine.Camera MainCamera => mainCamera;
    
        [SerializeField] private  UnityEngine.Camera mainCamera;
    }
}