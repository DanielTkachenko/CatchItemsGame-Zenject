using Zenject;

namespace CatchItemsGame.Installers
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().FromResource(ResourcesConst.PlayerConfig).AsSingle().NonLazy();
            Container.Bind<InputController>().AsSingle().NonLazy();
            Container.Bind<PlayerController>().AsSingle().NonLazy();
            Container.Bind<PlayerScoreCounter>().AsSingle().NonLazy();
            Container.BindFactory<PlayerView, PlayerView.Factory>().FromComponentInNewPrefabResource(ResourcesConst.PlayerPrefab);
        }
    }
}