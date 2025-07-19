using ClientServices;
using Zenject;

namespace Installers
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AddressablesAssetsService>().AsSingle();
        }
    }
}