using System;
using System.Collections.Generic;
using ClientServices;
using MVVM.Core.ViewModel;
using Services.Cards;
using Services.Level;
using Services.MatchingGame;
using UniRx;

namespace Views.World.Cards
{
    public class CardsViewModel : BaseViewModel, ICardsViewModel
    {
        public event Action GameStared;

        private readonly ICardsService _cardsService;
        private readonly IMatchingGameService _matchingGameService;
        private readonly IAssetsProvider _assetsProvider;
        private readonly ILevelService _levelService;

        private IReactiveCollection<ICardItemViewModel> _cardViewModels = new ReactiveCollection<ICardItemViewModel>();

        private List<ICardItemViewModel> _selectedCards = new();

        private CompositeDisposable _compositeDisposable = new();
        
        public IReadOnlyReactiveCollection<ICardItemViewModel> CardViewModels => _cardViewModels;
        public int Columns => _levelService.LevelItem.Value.Columns;
        public int Rows => _levelService.LevelItem.Value.Rows;

        public CardsViewModel(ICardsService cardsService,
            IMatchingGameService matchingGameService,
            IAssetsProvider assetsProvider,
            ILevelService levelService)
        {
            _cardsService = cardsService;
            _matchingGameService = matchingGameService;
            _assetsProvider = assetsProvider;
            _levelService = levelService;

            CreateCardViewModels(_cardsService.Cards);
            _cardsService.Cards.ObserveAdd().Subscribe(v=>CreateCardViewModel(v.Value)).AddTo(_compositeDisposable);
            _matchingGameService.OnGameStarted += OnGameStarted;
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
            cardViewModel.OnClick += CardViewModelOnClicked;
            _cardViewModels.Add(cardViewModel);
        }

        private void CardViewModelOnClicked(ICardItemViewModel cardItemViewModel)
        {
            _selectedCards.Add(cardItemViewModel);
            if (_selectedCards.Count < _levelService.LevelItem.Value.PairCount)
            {
                return;
            }

            var cardIds = new List<string>();

            foreach (var selectedCard in _selectedCards)
            {
                cardIds.Add(selectedCard.Id.Value);
            }
            
            _matchingGameService.Match(cardIds);
            
            _selectedCards.Clear();
        }

        private void OnGameStarted()
        {
            GameStared?.Invoke();
        }
        
        public override void ViewDestroyed()
        {
            Dispose();
            base.ViewDestroyed();
        }

        public override void Dispose()
        {
            foreach (var cardViewModel in _cardViewModels)
            {
                cardViewModel.OnClick -= CardViewModelOnClicked;
                cardViewModel.Dispose();
            }
            _matchingGameService.OnGameStarted -= OnGameStarted;
            _cardViewModels.Clear();
            _compositeDisposable.Dispose();
        }
    }
}