using Cysharp.Threading.Tasks;
using MVVM.Core.ViewModel;
using Services.MatchingGame;
using Views.Layer;
using Views.UI.MainMenu;
using Views.UI.MatchGameScreen;

namespace Views.UI.LosePopup
{
    public interface ILosePopupViewModel : IViewModel
    {
        void RestartLevel();
        UniTaskVoid MainMenu();
    }
    
    public class LosePopupViewModel : BaseViewModel, ILosePopupViewModel
    {
        private readonly IMatchingGameService _matchingGameService;

        public LosePopupViewModel(IMatchingGameService matchingGameService)
        {
            _matchingGameService = matchingGameService;
        }

        public void RestartLevel()
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