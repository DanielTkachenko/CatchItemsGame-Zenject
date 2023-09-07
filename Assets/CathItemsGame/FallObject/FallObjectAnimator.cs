using System;
using DG.Tweening;
using UnityEngine;

namespace CatchItemsGame
{
    public class FallObjectAnimator
    {
        public event Action<FallObjectView> DeathAnimationEnded; 

        private const float SpawnAnimDuration = 2.5f;
        private const float DeathAnimDuration = 0.1f;
    
        private readonly FallObjectController _fallObjectController;
    
        private Sequence _sequenceSpawn;
        private Sequence _sequenceDeath;

        public FallObjectAnimator(FallObjectController fallObjectController)
        {
            _fallObjectController = fallObjectController;
        }

        public void Spawn(FallObjectView view)
        {
            _sequenceSpawn?.Kill();

            _sequenceSpawn = DOTween.Sequence();

            _sequenceSpawn.Append(view.transform
                .DORotate(new Vector3(0.0f, 0.0f, 360.0f), SpawnAnimDuration, RotateMode.LocalAxisAdd)
                .SetLoops(-1, LoopType.Restart)
                .SetRelative()
                .SetEase(Ease.Linear));
        }

        public void Death(FallObjectView view)
        {
            _sequenceDeath?.Kill();

            _sequenceDeath = DOTween.Sequence();

            _sequenceDeath.Append(view.transform
                .DOScale(Vector3.zero, DeathAnimDuration)).
                OnComplete(() => DeathAnimationEnded?.Invoke(view));
        }
    }
}