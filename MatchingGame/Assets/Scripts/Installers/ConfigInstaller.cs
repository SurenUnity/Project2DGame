using Configs.Cards;
using Configs.Level;
using Configs.SOModels;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private GlobalConfigSO _globalConfigSo;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CardsConfigModel>()
                .FromInstance(_globalConfigSo.cardsSoConfigModel.cardsConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<LevelsConfigModel>()
                .FromInstance(_globalConfigSo.levelsSoConfigModel.levelsConfigModel).AsSingle();
        }
    }
}