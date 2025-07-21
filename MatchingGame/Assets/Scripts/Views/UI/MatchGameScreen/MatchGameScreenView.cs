using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using Views.UI.Core;

namespace Views.UI.MatchGameScreen
{
    public class MatchGameScreenView : ScreenViewBehaviour<IMatchGameScreenViewModel>
    {
        [SerializeField] private TMP_Text _scoreCountText;
        [SerializeField] private TMP_Text _triesLeftText;
        [SerializeField] private TMP_Text _levelText;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        protected override void OnViewModelBind()
        {
            ViewModel.TriesLeft.Subscribe(v => _triesLeftText.text = v.ToString()).AddTo(_compositeDisposable);
            ViewModel.Score.Subscribe(v => _scoreCountText.text = v.ToString()).AddTo(_compositeDisposable);
            ViewModel.Level.Subscribe(v => _levelText.text = v.ToString()).AddTo(_compositeDisposable);
        }

        public override void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}