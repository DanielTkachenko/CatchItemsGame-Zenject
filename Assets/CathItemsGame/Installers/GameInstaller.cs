using UnityEngine;
using Zenject;

namespace CatchItemsGame.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromComponentInNewPrefabResource(ResourcesConst.MainCamera).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
            
            SoundInstaller.Install(Container);
            UIInstaller.Install(Container);
            FallObjectInstaller.Install(Container);
            PlayerInstaller.Install(Container);
            
            
        }
    }
}