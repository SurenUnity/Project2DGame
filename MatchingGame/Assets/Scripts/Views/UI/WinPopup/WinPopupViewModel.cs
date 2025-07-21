using System;
using MVVM.Core.ViewModel;
using Services.Level;
using Services.MatchingGame;

namespace Views.UI.WinPopup
{
    public interface IWinPopupViewModel : IViewModel
    {
        void PlayNextLevel();
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

        public override void Dispose()
        {
        }
    }
}