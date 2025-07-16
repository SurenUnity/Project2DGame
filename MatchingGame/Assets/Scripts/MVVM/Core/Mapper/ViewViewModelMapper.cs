using System;
using System.Collections.Generic;
using MVVM.Core.View;
using MVVM.Core.ViewModel;

namespace MVVM.Core.Mapper
{
    public class ViewViewModelMapper : IViewViewModelMapper, IViewViewModelRegistrar
    {
        private Dictionary<Type, Type> _viewToViewModelMap = new();

        public void Register<TView, TViewModel>()
            where TViewModel : IBaseViewModel
            where TView : IViewInitializer
        {
            _viewToViewModelMap.TryAdd(typeof(TView), typeof(TViewModel));
        }
        
        public Type GetViewModelType(Type viewType)
        {
            if (!_viewToViewModelMap.TryGetValue(viewType, out var viewModelType))
            {
                throw new Exception($"Can not find corresponding view model type for {viewType.Name}");
            }
            
            if (viewModelType == null)
            {
                throw new Exception($"ViewModel is not registered for view type: {viewType}");
            }
            
            return viewModelType;
        }
    }
}