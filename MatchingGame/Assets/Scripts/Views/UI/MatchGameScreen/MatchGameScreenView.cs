using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Views.UI.Core;

namespace Views.UI.MatchGameScreen
{
    public class MatchGameScreenView : ScreenViewBehaviour<IMatchGameScreenViewModel>
    {
        [SerializeField] private TMP_Text _scoreCountText;
        [SerializeField] private TMP_Text _triesLeftText;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Button _quitLevelButton;

        private CompositeDisposable _compositeDisposable = new();
        
        protected override void OnViewModelBind()
        {
            ViewModel.TriesLeft.Subscribe(v => _triesLeftText.text = v.ToString()).AddTo(_compositeDisposable);
            ViewModel.Score.Subscribe(v => _scoreCountText.text = v.ToString()).AddTo(_compositeDisposable);
            ViewModel.Level.Subscribe(v => _levelText.text = v.ToString()).AddTo(_compositeDisposable);
            _quitLevelButton.onClick.AddListener(QuitLevelClickHandler);
        }

        private void QuitLevelClickHandler()
        {
            QuitLevel().Forget();
        }

        private async UniTaskVoid QuitLevel()
        {
            ViewModel.QuitLevel().Forget();
            await Hide();
        }

        public override void Dispose()
        {
            _quitLevelButton.onClick.RemoveListener(QuitLevelClickHandler);
            _compositeDisposable.Dispose();
        }
    }
}