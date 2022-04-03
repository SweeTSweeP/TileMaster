using UnityEngine;
using Wall;
using Zenject;

namespace Prefabs.Installers
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindWallBuilder();
        }

        private void BindWallBuilder()
        {
            Container.Bind<IWallBuilder>().To<WallBuilder>().AsSingle();
        }
    }
}