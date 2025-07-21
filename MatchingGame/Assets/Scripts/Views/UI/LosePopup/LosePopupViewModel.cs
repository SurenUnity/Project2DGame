using Cysharp.Threading.Tasks;
using MVVM.Core.ViewModel;
using Services.MatchingGame;

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
            //TODO Open main menu screen
        }

        public override void Dispose()
        {
        }
    }
}