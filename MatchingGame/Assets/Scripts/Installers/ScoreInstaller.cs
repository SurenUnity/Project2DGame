using Services.Score;
using Zenject;

namespace Installers
{
    public class ScoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle();
        }
    }
}