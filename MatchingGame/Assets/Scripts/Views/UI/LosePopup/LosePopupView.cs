using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Views.UI.Core;

namespace Views.UI.LosePopup
{
    public class LosePopupView : PopupViewBehaviour<ILosePopupViewModel>
    {
        [SerializeField] private Button _restartLevelButton;
        [SerializeField] private Button _mainMenuButton;
        
        protected override void OnViewModelBind()
        {
            _restartLevelButton.onClick.AddListener(RestartButtonClickHandler);
            _mainMenuButton.onClick.AddListener(MainMenuClickHandler);
        }

        private void RestartButtonClickHandler()
        {
            RestartLevel().Forget();
        }

        private void MainMenuClickHandler()
        {
            ViewModel.MainMenu().Forget();
        }

        private async UniTaskVoid RestartLevel()
        {
            ViewModel.RestartLevel();
            await Hide();
        }

        public override void Dispose()
        {
            _restartLevelButton.onClick.RemoveListener(RestartButtonClickHandler);
            _mainMenuButton.onClick.RemoveListener(MainMenuClickHandler);
        }
    }
}