using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ClientServices;
using Cysharp.Threading.Tasks;
using MVVM.Core.ViewModel;
using Services.Audio;
using Services.Cards;
using Services.Level;
using Services.MatchingGame;
using UniRx;
using Utils;

namespace Views.World.Cards
{
    public class CardsViewModel : BaseViewModel, ICardsViewModel
    {
        public event Action GameStared;

        private const float DeselectDelay = 0.7f;
        private const float MatchDelay = 0.5f;

        private readonly ICardsService _cardsService;
        private readonly IMatchingGameService _matchingGameService;
        private readonly IAssetsProvider _assetsProvider;
        private readonly ILevelService _levelService;
        private readonly IAudioManager _audioManager;

        private IReactiveCollection<ICardItemViewModel> _cardViewModels = new ReactiveCollection<ICardItemViewModel>();
        private List<ICardItemViewModel> _cardsForLevel = new();

        private Queue<ICardItemViewModel> _selectedCards = new();

        private CancellationTokenSource _delaysCancellationTokenSource = new();
        private CompositeDisposable _compositeDisposable = new();

        public IReadOnlyReactiveCollection<ICardItemViewModel> CardViewModels => _cardViewModels;
        public IReadOnlyList<ICardItemViewModel> CardsForLevel => _cardsForLevel;
        public int Columns => _levelService.LevelItem.Value.Columns;
        public int Rows => _levelService.LevelItem.Value.Rows;

        public CardsViewModel(ICardsService cardsService,
            IMatchingGameService matchingGameService,
            IAssetsProvider assetsProvider,
            ILevelService levelService,
            IAudioManager audioManager)
        {
            _cardsService = cardsService;
            _matchingGameService = matchingGameService;
            _assetsProvider = assetsProvider;
            _levelService = levelService;
            _audioManager = audioManager;

            CreateCardViewModels(_cardsService.Cards);
            _cardsService.Cards.ObserveAdd().Subscribe(v=>CreateCardViewModel(v.Value)).AddTo(_compositeDisposable);
            _matchingGameService.OnGameStarted += OnGameStarted;
            _matchingGameService.OnLevelQuit += OnLevelQuit;
        }

        private void OnLevelQuit()
        {
            _delaysCancellationTokenSource?.Cancel();
        }

        private void CreateCardViewModels(IEnumerable<ICardItem> cardItems)
        {
            foreach (var cardItem in cardItems)
            {
                CreateCardViewModel(cardItem);
            }
        }
        
        private void CreateCardViewModel(ICardItem cardItem)
        {
            var cardViewModel = new CardItemItemViewModel(cardItem, _assetsProvider);
            cardViewModel.OnClick += CardItemOnClicked;
            _cardViewModels.Add(cardViewModel);
        }
        
        private void CardItemOnClicked(ICardItemViewModel cardItemViewModel)
        {
            _audioManager.PlaySfx(AudioClipNames.Flip);
            _selectedCards.Enqueue(cardItemViewModel);
            SelectCard().Forget();
        }

        private async UniTaskVoid SelectCard()
        {
            var pairCount = _levelService.LevelItem.Value.PairCount;
            
            if (_selectedCards.Count < pairCount)
            {
                return;
            }

            var selectedDequeCards = new List<ICardItemViewModel>();
            var cardStaticIds = new List<int>();

            for (int i = 0; i < pairCount; i++)
            {
                var cardItemViewModel = _selectedCards.Dequeue();
                cardStaticIds.Add(cardItemViewModel.StaticId);
                selectedDequeCards.Add(cardItemViewModel);
            }
            
            var isSuccess = _matchingGameService.Match(cardStaticIds);

            if (isSuccess)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(MatchDelay), cancellationToken: _delaysCancellationTokenSource.Token)
                    .SuppressCancellationThrow();
                if (_delaysCancellationTokenSource.IsCancellationRequested)
                {
                    return;
                }
                
                _audioManager.PlaySfx(AudioClipNames.Match);
                
                foreach (var cardItemViewModel in selectedDequeCards)
                {
                    cardItemViewModel.Match();
                }
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(DeselectDelay), cancellationToken: _delaysCancellationTokenSource.Token)
                    .SuppressCancellationThrow();
                if (_delaysCancellationTokenSource.IsCancellationRequested)
                {
                    return;
                }
             
                _audioManager.PlaySfx(AudioClipNames.Mismatch);
                
                foreach (var cardItemViewModel in selectedDequeCards)
                {
                    if (_selectedCards.Contains(cardItemViewModel))
                    {
                        continue;
                    }
                    cardItemViewModel.Deselect();
                }
            }

            if (_selectedCards.Count >= pairCount)
            {
                SelectCard().Forget();
            }
        }

        private void OnGameStarted()
        {
            _delaysCancellationTokenSource = new();
            
            _cardsForLevel.Clear();
            
            foreach (var cardItem in _cardsService.CardsForLevel)
            {
                var cardViewModel = _cardViewModels.FirstOrDefault(v => v.StaticId == cardItem.StaticId);
                _cardsForLevel.Add(cardViewModel);
            }
            
            GameStared?.Invoke();
        }
        
        public override void ViewDestroyed()
        {
            Dispose();
            base.ViewDestroyed();
        }

        public override void Dispose()
        {
            _delaysCancellationTokenSource?.Cancel();
            foreach (var cardViewModel in _cardViewModels)
            {
                cardViewModel.OnClick -= CardItemOnClicked;
                cardViewModel.Dispose();
            }
            _matchingGameService.OnGameStarted -= OnGameStarted;
            _matchingGameService.OnLevelQuit -= OnLevelQuit;
            _cardViewModels.Clear();
            _compositeDisposable.Dispose();
        }
    }
}