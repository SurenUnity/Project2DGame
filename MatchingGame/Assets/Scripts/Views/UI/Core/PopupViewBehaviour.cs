using System;
using Cysharp.Threading.Tasks;
using MVVM.Core.View;
using MVVM.Core.ViewModel;
using UnityEngine;
using Views.UI.Components;

namespace Views.UI.Core
{
    [RequireComponent(typeof(PopupAnimationsComponent))]
    public abstract class PopupViewBehaviour<TViewModel> : BaseViewBehaviour<TViewModel> where TViewModel : IViewModel
    {
        [SerializeField] private PopupAnimationsComponent _popupAnimationsComponent;
        private bool _isShown;

        public override bool IsShown => _isShown;

        private void OnValidate()
        {
            _popupAnimationsComponent = GetComponent<PopupAnimationsComponent>();
        }

        public override async UniTask Show()
        {
            gameObject.SetActive(true);
            await _popupAnimationsComponent.ShowAnimation();
            ViewModel.ViewShowed();
            _isShown = true;
        }

        public override async UniTask Hide()
        {
            await _popupAnimationsComponent.HideAnimation();
            gameObject.SetActive(false);
            ViewModel.ViewHided();
            _isShown = false;
        }

        public override async UniTask Destroy()
        {
            await Hide();
            Dispose();
            ViewModel.ViewDestroyed();
            Destroy(gameObject);
        }

        public override void DestroyInstantly()
        {
            _popupAnimationsComponent.Dispose();
            Dispose();
            ViewModel.ViewDestroyed();
            Destroy(gameObject);
        }
    }
}