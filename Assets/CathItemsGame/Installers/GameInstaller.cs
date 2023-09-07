using UnityEngine;
using Zenject;

namespace CatchItemsGame.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //base.InstallBindings();
            Container.Bind<SoundController>().AsSingle().NonLazy();
            Container.Bind<Camera>().FromComponentInNewPrefabResource("Main Camera").AsSingle().NonLazy();
            
            Container.Bind<UIService>().AsSingle().NonLazy();

            Container.Bind<UIMainMenuWindowController>().AsSingle().NonLazy();
            Container.Bind<UIGameWindowController>().AsSingle().NonLazy();
            Container.Bind<UIEndGameWindowController>().AsSingle().NonLazy();
            Container.Bind<HUDWindowController>().AsSingle().NonLazy();

            Container.Bind<InputController>().AsSingle().NonLazy();
            Container.Bind<PlayerController>().AsSingle().NonLazy();
            Container.Bind<FallObjectSpawner>().AsSingle();
            Container.Bind<PlayerScoreCounter>().AsSingle().NonLazy();
            Container.Bind<TickableManager>().FromComponentInNewPrefabResource("TickableManager").AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();

            Container.Bind<FallObjectController>().AsSingle().NonLazy();
            Container.BindMemoryPool<FallObjectView, FallObjectView.Pool>().WithInitialSize(10).FromComponentInNewPrefabResource("FallObject");
        }
    }
}