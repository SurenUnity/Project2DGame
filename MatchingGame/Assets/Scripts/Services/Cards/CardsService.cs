using System;
using System.Collections.Generic;
using System.Linq;
using Configs.Cards;
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
        
        public IReadOnlyReactiveCollection<ICardItem> Cards => _cardItems;

        public CardsService(CardsConfigModel cardsConfigModel, 
            ILevelService levelService)
        {
            _cardsConfigModel = cardsConfigModel;
            _levelService = levelService;

            _levelService.LevelItem.Subscribe(CreateCards).AddTo(_disposable);
        }

        public bool TryMatch(List<string> cardIds)
        {
            var isMatched = false;

            var cardItems = _cardItems
                .Where(card => cardIds.Contains(card.Id.Value))
                .ToList();
            
            for (int i = 0; i < cardItems.Count; i++)
            {
                if (i + 1 >= cardItems.Count)
                {
                    break;
                }

                var firstCard = _cardItems[i];
                var secondCard = _cardItems[i + 1];
                
                isMatched = firstCard.Id == secondCard.Id;
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
            return _cardItems.All(v => v.StateType.Value is CardStateType.Matched);
        }

        public void PrepareCardsToPlay()
        {
            foreach (var cardItem in _cardItems)
            {
                cardItem.Enable();
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
                    cardItem = new CardItem();
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