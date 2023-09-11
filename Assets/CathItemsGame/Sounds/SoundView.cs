using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CatchItemsGame
{
    public class SoundView : MonoBehaviour
    {
        public AudioSource AudioSource => audioSource;

        [SerializeField] private AudioSource audioSource;

        public class Pool : MemoryPool<SoundModel, SoundView>
        {
            private List<SoundView> _views;

            public Pool()
            {
                _views = new List<SoundView>();
            }
            protected override void OnCreated(SoundView item)
            {
                //base.OnCreated(item);
                _views.Add(item);
            }

            protected override void Reinitialize(SoundModel model, SoundView item)
            {
                item.gameObject.SetActive(true);
                item.AudioSource.clip = model.Clip;
                item.AudioSource.volume = model.Volume;
                item.AudioSource.loop = model.Loop;
            }

            protected override void OnDespawned(SoundView item)
            {
                item.AudioSource.clip = null;
                item.gameObject.SetActive(false);
            }

            public void DisableCompletedSounds()
            {
                foreach (var soundView in _views)
                {
                    
                    if (!soundView.AudioSource.isPlaying && soundView.gameObject.activeInHierarchy)
                    {
                        Despawn(soundView);
                    }
                }
            }

            public void MuteSound()
            {
                foreach (var soundView in _views)
                {
                    if (soundView.gameObject.activeInHierarchy)
                    {
                        Despawn(soundView);
                    }
                }
            }
        }
    }
}