using Cysharp.Threading.Tasks;
using MVVM.Core.ViewModel;
using Services.Audio;
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
        private readonly IAudioManager _audioManager;

        public LosePopupViewModel(IMatchingGameService matchingGameService,
            IAudioManager audioManager)
        {
            _matchingGameService = matchingGameService;
            _audioManager = audioManager;
        }

        public override void ViewShowed()
        {
            base.ViewShowed();
            
            _audioManager.PlaySfx(AudioClipNames.Lose);
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