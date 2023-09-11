using UnityEngine;
using Zenject;

namespace CatchItemsGame.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //base.InstallBindings();
            Container.Bind<FallObjectConfig>().FromResource(ResourcesConst.FallObjectConfigPath).AsSingle().NonLazy();
            Container.Bind<PlayerConfig>().FromResource(ResourcesConst.PlayerConfig).AsSingle().NonLazy();
            Container.Bind<FallObjectSpawnConfig>().FromResource(ResourcesConst.FallObjectSpawnConfig).AsSingle().NonLazy();
            Container.Bind<SoundConfig>().FromResource(ResourcesConst.SoundConfig).AsSingle().NonLazy();
            
            Container.Bind<SoundController>().AsSingle().NonLazy();
            Container.BindMemoryPool<SoundView, SoundView.Pool>().FromComponentInNewPrefabResource(ResourcesConst.SoundView);
            
            Container.Bind<Camera>().FromComponentInNewPrefabResource("Main Camera").AsSingle().NonLazy();

            Container.Bind<UIRoot>().FromComponentInNewPrefabResource("UIRoot").AsSingle().NonLazy();
            Container.Bind<UIService>().AsSingle().NonLazy();

            Container.Bind<UIMainMenuWindowController>().AsSingle().NonLazy();
            Container.Bind<UIGameWindowController>().AsSingle().NonLazy();
            Container.Bind<UIEndGameWindowController>().AsSingle().NonLazy();
            Container.Bind<HUDWindowController>().AsSingle().NonLazy();

            Container.Bind<InputController>().AsSingle().NonLazy();
            Container.Bind<PlayerController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FallObjectSpawner>().AsSingle();
            Container.Bind<PlayerScoreCounter>().AsSingle().NonLazy();
            Container.Bind<TickableManager>().FromComponentInNewPrefabResource("TickableManager").AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<FallObjectController>().AsSingle().NonLazy();
            Container.BindMemoryPool<FallObjectView, FallObjectView.Pool>().WithInitialSize(10).FromComponentInNewPrefabResource("FallObject");
            Container.BindFactory<PlayerView, PlayerView.Factory>().FromComponentInNewPrefabResource("Player");
        }
    }
}