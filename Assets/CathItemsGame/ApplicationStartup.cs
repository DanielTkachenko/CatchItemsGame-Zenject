using UnityEngine;

namespace CatchItemsGame
{
    public class ApplicationStartup : MonoBehaviour
    {
        private IBootstrap _bootstrap = new Bootstrap();
        private UnityEngine.Camera _camera;

        private void Start()
        {
            StartBootstrap();
            
            var gameController = new GameController(_camera);
            gameController.InitGame();
        }

        private void StartBootstrap()
        {
            _bootstrap.Add(new CreateMainCameraCommand(out _camera));
            _bootstrap.Add(new CreateTickableManagerCommand());

            _bootstrap.OnExecuteAllComandsNotify += NotifyOfCompletion;
            _bootstrap.Execute();
        }

        private void NotifyOfCompletion()
        { }
    }
}
