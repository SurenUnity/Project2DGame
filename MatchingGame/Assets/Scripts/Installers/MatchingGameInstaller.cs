using Services.MatchingGame;
using Zenject;

namespace Installers
{
    public class MatchingGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MatchingGameService>().AsSingle();
        }
    }
}