using MVVM;
using MVVM.Core;
using MVVM.Core.Factory;
using MVVM.Core.Mapper;
using UnityEngine;
using Views.World.Cards;
using Zenject;

namespace Installers
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField] private ViewLayerModel _layerModel;
        
        private ViewViewModelMapper _viewViewModelMapper;

        public override void InstallBindings()
        {
            BindMvvm();
            RegisterWorldViews();
        }

        private void BindMvvm()
        {
            _viewViewModelMapper = new ViewViewModelMapper();
            Container.BindInterfacesAndSelfTo<ViewViewModelMapper>().FromInstance(_viewViewModelMapper);
            Container.BindInterfacesAndSelfTo<ViewLayerModel>().FromInstance(_layerModel).AsSingle();
            Container.BindInterfacesAndSelfTo<ViewViewModelFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ViewManager>().AsSingle();
        }

        private void RegisterWorldViews()
        {
            _viewViewModelMapper.Register<CardsView, CardsViewModel>();
        }
    }
}