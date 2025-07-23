using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Services.Cards
{
    public interface ICardsService : IDisposable
    {
        IReadOnlyReactiveCollection<ICardItem> Cards { get; }
        IReadOnlyList<ICardItem> CardsForLevel { get; }
        UniTask InitAsync();
        bool TryMatch(List<int> cardStaticIds);
        void PrepareCardsToPlay();
        bool IsAllCardsMatched();
    }
}