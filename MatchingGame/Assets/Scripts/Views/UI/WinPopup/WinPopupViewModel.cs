using System;
using Cysharp.Threading.Tasks;
using MVVM.Core.ViewModel;
using Services.Level;
using Services.MatchingGame;
using Views.Layer;
using Views.UI.MainMenu;
using Views.UI.MatchGameScreen;

namespace Views.UI.WinPopup
{
    public interface IWinPopupViewModel : IViewModel
    {
        void PlayNextLevel();
        UniTaskVoid MainMenu();
    }
    
    public class WinPopupViewModel : BaseViewModel, IWinPopupViewModel
    {
        private readonly IMatchingGameService _matchingGameService;

        public WinPopupViewModel(
            IMatchingGameService matchingGameService)
        {
            _matchingGameService = matchingGameService;
        }

        public void PlayNextLevel()
        {
            _matchingGameService.StartGame();
        }

        public async UniTaskVoid MainMenu()
        {
            _matchingGameService.QuitLevel();
            await ViewManager.ShowAsync<MainMenuScreenView>(LayerNames.Screen);
            await ViewManager.HideAsync<MatchGameScreenView>();
        }
        
        public override void Dispose()
        {
        }
    }
}