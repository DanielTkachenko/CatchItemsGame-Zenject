using UnityEngine;
using Zenject;

namespace CatchItemsGame.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromComponentInNewPrefabResource("Main Camera").AsSingle().NonLazy();
            Container.Bind<TickableManager>().FromComponentInNewPrefabResource(ResourcesConst.TickableManager).AsSingle().NonLazy();
            
            SoundInstaller.Install(Container);
            UIInstaller.Install(Container);
            FallObjectInstaller.Install(Container);
            PlayerInstaller.Install(Container);
            
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
        }
    }
}