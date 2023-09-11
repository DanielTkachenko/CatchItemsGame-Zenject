using UnityEngine;
using Zenject;

namespace CatchItemsGame
{
    public class GameController : IInitializable
    {
        private FallObjectSpawner _fallObjectSpawner;
        private PlayerController _playerController;
        private UIService _uiService;
        private UIGameWindowController _gameWindowController;
        private SoundController _soundController;
        
        public GameController(
            UIService uiService,
            PlayerController playerController,
            SoundController soundController,
            FallObjectSpawner fallObjectSpawner,
            UIGameWindowController gameWindowController
        )
        {
            _uiService = uiService;
            _playerController = playerController;
            _soundController = soundController;
            _fallObjectSpawner = fallObjectSpawner;
            _gameWindowController = gameWindowController;

            playerController.PlayerHpController.OnZeroHealth += StopGame;
        }
        
        
        public void Initialize()
        {
            InitGame();
        }

        public void InitGame()
        {
            _uiService.Show<UIMainMenuWindow>();
            
            _soundController.Play(SoundName.BackStart);
        }

        public void StartGame()
        {
            _soundController.Stop();
            _soundController.Play(SoundName.BackMain);
            
            _playerController.Spawn();
            _fallObjectSpawner.StartSpawn();
        }

        public void StopGame()
        {
            _playerController.DestroyView(()=>_gameWindowController.ShowEndMenuWindow());
            _fallObjectSpawner.StopSpawn();
        }
    }
}
