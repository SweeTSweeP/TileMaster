using Tile;
using Wall;
using Zenject;

namespace Prefabs.Installers
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindWallBuilder();
            BindTilePlacer();
        }

        private void BindWallBuilder() => 
            Container.Bind<IWallBuilder>().To<WallBuilder>().AsSingle();
        

        private void BindTilePlacer() =>
            Container.Bind<ITilePlacer>().To<TilePlacer>().AsSingle();
    }
}