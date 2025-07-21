using Cysharp.Threading.Tasks;
using MVVM.Core;
using Services.MatchingGame;
using Views.Layer;
using Views.UI.MainMenu;
using Views.UI.MatchGameScreen;
using Views.World.Cards;

namespace Services
{
    public interface IGameStartupService
    {
        UniTaskVoid Startup();
    }
    
    public class GameStartupService : IGameStartupService
    {
        private readonly IViewManager _viewManager;

        public GameStartupService(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        public async UniTaskVoid Startup()
        {
            await _viewManager.ShowAsync<MainMenuScreenView>(LayerNames.Screen);
        }
    }
}