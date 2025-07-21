using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MVVM.Core.Factory;
using MVVM.Core.Mapper;
using MVVM.Core.View;
using MVVM.Core.ViewModel;
using UnityEngine;
using Logger = Utils.Logger;

namespace MVVM.Core
{
    public interface IViewManager
    {
        UniTask<IViewModel> ShowAsync<TView>(string layer, string viewName = "") where TView : IViewInitializer;
        UniTask HideAsync<TView>(string viewName = "") where TView : IView;
        UniTask DestroyAsync<TView>(string viewName = "") where TView : IView;
        void DestroyInstantly<TView>(string viewName = "") where TView : IView;
    }
    
    public class ViewManager : IViewManager
    {
        private readonly ViewLayerModel _layerModel;
        private readonly IViewViewModelMapper _mapper;
        private readonly IViewViewModelFactory _factory;

        private HashSet<IViewInitializer> _views = new();
        private HashSet<IBaseViewModel> _viewModels = new();

        public ViewManager(ViewLayerModel layerModel,
            IViewViewModelMapper mapper,
            IViewViewModelFactory factory)
        {
            _layerModel = layerModel;
            _mapper = mapper;
            _factory = factory;
        }
        
        public async UniTask<IViewModel> ShowAsync<TView>(string layer, string viewName = "") where TView : IViewInitializer
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = typeof(TView).Name;
            }
            
            var viewModelType = _mapper.GetViewModelType(typeof(TView));
            
            var viewModel = _viewModels.FirstOrDefault(v => v.GetType() == viewModelType);
            if (viewModel == null)
            {
                viewModel = CreateViewModel<TView>();
                viewModel.SetViewManager(this);
            }
            
            var view = _views.FirstOrDefault(v=>v.GetType() == typeof(TView));
            if (view == null)
            {
                view = await CreateViewAsync<TView>(layer, viewName);
                view.SetViewModel(viewModel);
                view.SetName(viewName);
            }

            await view.Show();
            
            return viewModel;
        }

        public async UniTask HideAsync<TView>(string viewName = "") where TView : IView
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = typeof(TView).Name;
            }
            
            var view = _views.FirstOrDefault(v => v.Name == viewName);
            if (view == null)
            {
                Logger.LogError($"View {viewName} not found");
                return;
            }
            
            await view.Hide();
        }

        public async UniTask DestroyAsync<TView>(string viewName = "") where TView : IView
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = typeof(TView).Name;
            }
            
            var view = _views.FirstOrDefault(v => v.Name == viewName);
            if (view == null)
            {
                Logger.LogError($"View {viewName} not found");
                return;
            }

            await view.Destroy();
        }
        
        public void DestroyInstantly<TView>(string viewName = "") where TView : IView
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = typeof(TView).Name;
            }
            
            var view = _views.FirstOrDefault(v => v.Name == viewName);
            if (view == null)
            {
                Logger.LogError($"View {viewName} not found");
                return;
            }

            view.DestroyInstantly();
        }

        private async UniTask<IViewInitializer> CreateViewAsync<TView>(string layer, string viewName) where TView : IViewInitializer
        {
            var layerTransform = _layerModel.GetLayer(layer);
            var view = await _factory.CreateView<TView>(viewName, layerTransform);
            _views.Add(view);
            return view;
        }

        private IBaseViewModel CreateViewModel<TView>() where TView : IViewInitializer
        {
            var viewModel = _factory.CreateViewModel<TView>();
            _viewModels.Add(viewModel);
            return viewModel;
        }
    }
}