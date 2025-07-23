using ClientServices;
using Services.Audio;
using Services.StateSaver;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private AudioManager _audioManager;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AudioManager>().FromInstance(_audioManager).AsSingle();
            Container.BindInterfacesAndSelfTo<AddressablesAssetsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ApplicationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<JsonStateSaveLoadBehaviour>().AsSingle();
            Container.Bind(typeof(IStateSaver<>)).To(typeof(StateSaver<>)).AsTransient();
        }
    }
}