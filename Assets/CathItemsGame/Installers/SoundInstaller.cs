using Zenject;

namespace CatchItemsGame.Installers
{
    public class SoundInstaller : Installer<SoundInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SoundConfig>()
                .FromResource(ResourcesConst.SoundConfig)
                .AsSingle()
                .NonLazy();
            Container.Bind<SoundController>()
                .AsSingle()
                .NonLazy();
            Container.BindMemoryPool<SoundView, SoundView.Pool>()
                .FromComponentInNewPrefabResource(ResourcesConst.SoundView)
                .UnderTransformGroup("SoundPool");
        }
    }
}