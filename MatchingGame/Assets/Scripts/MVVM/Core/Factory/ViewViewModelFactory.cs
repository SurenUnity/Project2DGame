using System;
using ClientServices;
using Cysharp.Threading.Tasks;
using MVVM.Core.Mapper;
using MVVM.Core.View;
using MVVM.Core.ViewModel;
using UnityEngine;
using Zenject;
using Logger = Utils.Logger;

namespace MVVM.Core.Factory
{
    public interface IViewViewModelFactory
    {
        IBaseViewModel CreateViewModel<TView>() where TView : IViewInitializer;

        UniTask<IViewInitializer> CreateView<TView>(string viewName, Transform layerContainer)
            where TView : IViewInitializer;

        UniTask<Tuple<IBaseViewModel, IViewInitializer>> CreateViewAndViewModel<TView>(string viewName,
            Transform layerContainer)
            where TView : IViewInitializer;
    }
    
    public class ViewViewModelFactory : IViewViewModelFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IViewViewModelMapper _mapper;
        private readonly IAssetsProvider _assetsProvider;

        public ViewViewModelFactory(
            IInstantiator instantiator,
            IViewViewModelMapper mapper,
            IAssetsProvider assetsProvider)
        {
            _instantiator = instantiator;
            _mapper = mapper;
            _assetsProvider = assetsProvider;
        }

        public IBaseViewModel CreateViewModel<TView>() where TView : IViewInitializer
        {
            var viewModel = _mapper.GetViewModelType(typeof(TView));
            return _instantiator.Instantiate(viewModel) as IBaseViewModel;
        }

        public async UniTask<IViewInitializer> CreateView<TView>(string viewName, Transform layerContainer) where TView : IViewInitializer
        {
            var viewPrefab = await _assetsProvider.GetPrefab(viewName);
            if (viewPrefab == null)
            {
                throw new Exception($"View {viewName} was not found");
            }

            var viewObject = _instantiator.InstantiatePrefab(viewPrefab, layerContainer);
            return viewObject.GetComponent<TView>();
        }

        public async UniTask<Tuple<IBaseViewModel, IViewInitializer>> CreateViewAndViewModel<TView>(string viewName,
            Transform layerContainer)
            where TView : IViewInitializer
        {
            var viewModel = CreateViewModel<TView>();
            var view = await CreateView<TView>(viewName, layerContainer);
            return new Tuple<IBaseViewModel, IViewInitializer>(viewModel, view);
        }
    }
}