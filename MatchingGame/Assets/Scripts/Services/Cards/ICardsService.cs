using System;
using System.Collections.Generic;
using UniRx;

namespace Services.Cards
{
    public interface ICardsService : IDisposable
    {
        IReadOnlyReactiveCollection<ICardItem> Cards { get; }
        bool TryMatch(List<int> cardStaticIds);
        void PrepareCardsToPlay();
        bool IsAllCardsMatched();
    }
}