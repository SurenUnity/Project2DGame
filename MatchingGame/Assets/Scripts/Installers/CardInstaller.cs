using Services.Cards;
using Zenject;

namespace Installers
{
    public class CardInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CardsService>().AsSingle();
        }
    }
}