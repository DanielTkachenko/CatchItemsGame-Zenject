using Zenject;

namespace CatchItemsGame.Installers
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<UIRoot>()
                .FromComponentInNewPrefabResource(ResourcesConst.UIRoot)
                .AsSingle()
                .NonLazy();
            Container.Bind<UIService>()
                .AsSingle()
                .NonLazy();

            Container.Bind<UIMainMenuWindowController>()
                .AsSingle()
                .NonLazy();
            Container.Bind<UIGameWindowController>()
                .AsSingle()
                .NonLazy();
            Container.Bind<UIEndGameWindowController>()
                .AsSingle()
                .NonLazy();
            Container.Bind<HUDWindowController>()
                .AsSingle()
                .NonLazy();
        }
    }
}