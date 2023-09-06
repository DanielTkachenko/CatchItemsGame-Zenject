using UnityEngine;

namespace CatchItemsGame
{
    public class SoundView : MonoBehaviour
    {
        public AudioSource AudioSource => audioSource;

        [SerializeField] private AudioSource audioSource;
    }
}