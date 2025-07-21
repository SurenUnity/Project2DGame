using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Views.UI.Core;

namespace Views.UI.WinPopup
{
    public class WinPopupView : PopupViewBehaviour<IWinPopupViewModel>
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _mainMenuButton;
        
        protected override void OnViewModelBind()
        {
            _nextLevelButton.onClick.AddListener(NextLevelClickHandler);
            _mainMenuButton.onClick.AddListener(MainMenuClickHandler);
        }

        private void NextLevelClickHandler()
        {
            PlayNextLevel().Forget();
        }

        private void MainMenuClickHandler()
        {
            MainMenu().Forget();
        }

        private async UniTaskVoid PlayNextLevel()
        {
            ViewModel.PlayNextLevel();
            await Hide();
        }

        private async UniTaskVoid MainMenu()
        {
            ViewModel.MainMenu().Forget();
            await Hide();
        }
        
        public override void Dispose()
        {
            _nextLevelButton.onClick.RemoveListener(NextLevelClickHandler);
            _mainMenuButton.onClick.RemoveListener(MainMenuClickHandler);
        }
    }
}