using System;
using System.Collections.Generic;
using UniRx;

namespace Services.MatchingGame
{
    public interface IMatchingGameService
    {
        event Action OnGameStarted;
        event Action OnWin;
        event Action OnLose;
        IReadOnlyReactiveProperty<int> TriesLeftCount { get; }
        void StartGame();
        bool Match(List<int> cardStaticIds);
    }
}