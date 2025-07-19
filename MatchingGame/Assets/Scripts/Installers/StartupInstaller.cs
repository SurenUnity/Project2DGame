using Services;
using Zenject;

namespace Installers
{
    public class StartupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameStartupService>().AsSingle();
        }
    }
}