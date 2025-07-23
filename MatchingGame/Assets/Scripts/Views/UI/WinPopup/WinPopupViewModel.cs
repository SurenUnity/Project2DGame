using System;
using Cysharp.Threading.Tasks;
using MVVM.Core.ViewModel;
using Services.Audio;
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
        private readonly IAudioManager _audioManager;

        public WinPopupViewModel(
            IMatchingGameService matchingGameService,
            IAudioManager audioManager)
        {
            _matchingGameService = matchingGameService;
            _audioManager = audioManager;
        }

        public override void ViewShowed()
        {
            base.ViewShowed();
            
            _audioManager.PlaySfx(AudioClipNames.Win);
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