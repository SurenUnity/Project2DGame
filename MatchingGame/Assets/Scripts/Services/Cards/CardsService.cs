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
    public interface ICardsService : IDisposable
    {
        
    }
    
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
            
            cardIds.Shuffle();

            foreach (var cardId in cardIds)
            {
                var cardConfigModel = _cardsConfigModel.cards.FirstOrDefault(v => v.id == cardId);
                    
                if (cardConfigModel == null)
                {
                    Logger.LogError($"Card config for ID {cardId} not found.");
                    return;
                }
                
                var cardItem = new CardItem(cardConfigModel);
                _cardItems.Add(cardItem);
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}