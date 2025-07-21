using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Views.UI.Core;

namespace Views.UI.WinPopup
{
    public class WinPopupView : PopupViewBehaviour<IWinPopupViewModel>
    {
        [SerializeField] private Button _nextLevelButton;
        
        protected override void OnViewModelBind()
        {
            _nextLevelButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            PlayNextLevel().Forget();
        }

        private async UniTaskVoid PlayNextLevel()
        {
            ViewModel.PlayNextLevel();
            await Hide();
        }
        
        public override void Dispose()
        {
            _nextLevelButton.onClick.RemoveListener(OnClick);
        }
    }
}