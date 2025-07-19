using Services.Level;
using Zenject;

namespace Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelService>().AsSingle();
        }
    }
}