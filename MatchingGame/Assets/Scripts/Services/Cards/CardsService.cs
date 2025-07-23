using System;
using System.Collections.Generic;
using System.Linq;
using Configs.Cards;
using Cysharp.Threading.Tasks;
using Extensions;
using Services.Level;
using UniRx;
using Utils;

namespace Services.Cards
{
    public class CardsService : ICardsService
    {
        private readonly CardsConfigModel _cardsConfigModel;
        private readonly ILevelService _levelService;

        private CompositeDisposable _disposable = new();
        
        private IReactiveCollection<ICardItem> _cardItems = new ReactiveCollection<ICardItem>();
        private List<ICardItem> _cardsForLevel = new();
        
        public IReadOnlyReactiveCollection<ICardItem> Cards => _cardItems;
        public IReadOnlyList<ICardItem> CardsForLevel => _cardsForLevel;

        public CardsService(CardsConfigModel cardsConfigModel, 
            ILevelService levelService)
        {
            _cardsConfigModel = cardsConfigModel;
            _levelService = levelService;
        }

        public async UniTask InitAsync()
        {
            _levelService.LevelItem.Subscribe(CreateCards).AddTo(_disposable);
        }

        public bool TryMatch(List<int> cardStaticIds)
        {
            var isMatched = false;

            var cardItems = _cardItems
                .Where(card => cardStaticIds.Contains(card.StaticId))
                .ToList();
            
            for (int i = 0; i < cardItems.Count; i++)
            {
                if (i + 1 >= cardItems.Count)
                {
                    break;
                }

                var firstCard = cardItems[i];
                var secondCard = cardItems[i + 1];
                
                isMatched = firstCard.Id.Value == secondCard.Id.Value;
                if (isMatched == false)
                {
                    break;
                }
            }

            foreach (var cardItem in cardItems)
            {
                if (isMatched)
                {
                    cardItem.Match();
                }
                else
                {
                    cardItem.Deselect();
                }
            }

            return isMatched;
        }

        public bool IsAllCardsMatched()
        {
            return _cardsForLevel.All(v => v.StateType.Value is CardStateType.Matched);
        }

        public void PrepareCardsToPlay()
        {
            _cardsForLevel.Clear();
            
            foreach (var cardItem in _cardItems)
            {
                cardItem.Disable();
            }
            
            foreach (var cardId in _levelService.LevelItem.Value.CardIds)
            {
                for (int i = 0; i < _levelService.LevelItem.Value.PairCount; i++)
                {
                    var cardItem = _cardItems.FirstOrDefault(v =>
                        v.Id.Value == cardId && v.StateType.Value is CardStateType.Disable);
                    cardItem?.Enable();
                    _cardsForLevel.Add(cardItem);
                }
            }
        }
        
        private void CreateCards(ILevelItem levelItem)
        {
            var cardsCount = levelItem.Columns * levelItem.Rows;

            if (cardsCount % levelItem.PairCount != 0)
            {
                Logger.LogError($"{levelItem} has invalid number of columns {levelItem.Columns} and/or rows {levelItem.Rows}");
                return;
            }
            
            if (levelItem.CardIds.Length < levelItem.PairCount)
            {
                Logger.LogError($"Not enough cards for level {levelItem.Level}, nee at least {levelItem.PairCount} but got {levelItem.CardIds.Length}");
                return;
            }

            var cardIds = new List<string>();
            
            for (int i = 0; i < levelItem.CardIds.Length; i++)
            {
                var cardId = levelItem.CardIds[i];
                
                for (int j = 0; j < levelItem.PairCount; j++)
                {
                    cardIds.Add(cardId);
                }
            }

            foreach (var cardItem in _cardItems)
            {
                cardItem.Disable();
            }

            for (var index = 0; index < cardIds.Count; index++)
            {
                var cardId = cardIds[index];
                var cardConfigModel = _cardsConfigModel.cards.FirstOrDefault(v => v.id == cardId);
                
                if (cardConfigModel == null)
                {
                    Logger.LogError($"Card config for ID {cardId} not found.");
                    return;
                }

                ICardItem cardItem;

                if (index >= _cardItems.Count)
                {
                    var staticId = 0;
                    if (_cardItems.Count > 0)
                    {
                        staticId = _cardItems.Max(v => v.StaticId) + 1;
                    }
                    cardItem = new CardItem(staticId);
                    _cardItems.Add(cardItem);
                }
                
                cardItem = _cardItems[index];

                cardItem.Init(cardConfigModel);
                cardItem.Enable();
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}