using System;
using Cysharp.Threading.Tasks;
using MVVM.Core.ViewModel;
using UnityEngine;

namespace MVVM.Core.View
{
    public abstract class BaseViewBehaviour<TViewModel> : MonoBehaviour, IViewInitializer, IDisposable where TViewModel : IViewModel
    {
        public string Name { get; private set; }
        public abstract bool IsShown { get; }
        public Type ViewModelType => typeof(TViewModel);

        protected TViewModel ViewModel;

        public void SetName(string viewName)
        {
            Name = viewName;
        }
        
        public void SetViewModel(IViewModel viewModel)
        {
            ViewModel = (TViewModel)viewModel;
            OnViewModelBind();
        }
        
        protected abstract void OnViewModelBind();
        
        public abstract UniTask Show();
        
        public abstract UniTask Hide();
        
        public abstract UniTask Destroy();
        
        public abstract void DestroyInstantly();
        public abstract void Dispose();
    }
}