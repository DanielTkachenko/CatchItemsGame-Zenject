using Zenject;

namespace CatchItemsGame.Installers
{
    public class FallObjectInstaller : Installer<FallObjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<FallObjectConfig>()
                .FromResource(ResourcesConst.FallObjectConfigPath)
                .AsSingle()
                .NonLazy();
            Container.Bind<FallObjectSpawnConfig>()
                .FromResource(ResourcesConst.FallObjectSpawnConfig)
                .AsSingle()
                .NonLazy();
            Container.Bind<FallObjectSpawner>().AsSingle();
            Container.Bind<FallObjectController>().AsSingle().NonLazy();
            Container.BindMemoryPool<FallObjectView, FallObjectView.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefabResource(ResourcesConst.FallObjectViewPath)
                .UnderTransformGroup("FallObjectPool");
        }
    }
}