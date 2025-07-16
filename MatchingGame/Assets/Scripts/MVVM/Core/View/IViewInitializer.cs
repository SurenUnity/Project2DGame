using System;
using MVVM.Core.ViewModel;

namespace MVVM.Core.View
{
    public interface IViewInitializer : IView
    {
        Type ViewModelType { get; }
        void SetName(string viewName);
        void SetViewModel(IViewModel viewModel);
    }
}