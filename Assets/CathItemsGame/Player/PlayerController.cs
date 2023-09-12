using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CatchItemsGame
{
    public class PlayerController
    {
        public const float DelayDestroyPlayer = 2f;
        
        public Action OnDisposed;
        public event Action<float> OnChangeSpeed;
        public PlayerHpController PlayerHpController => _playerHpController;

        private SoundController _soundController;
        private InputController _inputController;
        private PlayerConfig _playerConfig;
        private PlayerView _playerView;
        private PlayerHpController _playerHpController;
        private PlayerScoreCounter _playerScoreCounter;
        private PlayerView.Factory _playerFactory;
        private PlayerMovementController _playerMovementController;
        private PlayerAnimator _playerAnimator;
        private Camera _camera;
        
        private float _currentSpeed;

        public PlayerController(
            InputController inputController,
            HUDWindowController hudWindowController,
            Camera camera,
            SoundController soundController,
            PlayerView.Factory playerFactory,
            PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;

            _soundController = soundController;
            _inputController = inputController;
            _camera = camera;
            _playerFactory = playerFactory;
            
            _playerHpController = new PlayerHpController(_playerConfig.PlayerModel.Health, _soundController);
            _playerHpController.OnHealthChanged += hudWindowController.ChangeHealthPoint;
            
            _playerScoreCounter = new PlayerScoreCounter(soundController);
            _playerScoreCounter.ScoreChangeNotify += hudWindowController.ChangePlayerScore;
        }
        
        public PlayerView Spawn()
        {
            var model = _playerConfig.PlayerModel;
            _currentSpeed = model.Speed;
            _playerView = _playerFactory.Create();
            _playerAnimator = new PlayerAnimator(_playerView, _camera);
            _playerAnimator.Spawn();
            _playerMovementController = new PlayerMovementController(_inputController, _playerView, this);
            _playerHpController.SetHealth(_playerConfig.PlayerModel.Health);
            _playerScoreCounter.SetScores();
            
            return _playerView;
        }

        public void SetSpeed(float newSpeed)
        {
            _currentSpeed = newSpeed;

            OnChangeSpeed?.Invoke(_currentSpeed);
        }

        public void DestroyView(DG.Tweening.TweenCallback setEndWindow = null)
        {
            OnDisposed?.Invoke();
            
            _soundController.Stop();
            _soundController.Play(SoundName.GameOver);

            _playerAnimator.Death(setEndWindow);
            Zenject.GameObjectContext.Destroy(_playerView.gameObject, DelayDestroyPlayer);
            _playerView = null;
        }
    }
}