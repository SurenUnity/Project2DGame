using System;
using System.Collections.Generic;
using UniRx;

namespace Services.MatchingGame
{
    public interface IMatchingGameService
    {
        event Action OnWin;
        event Action OnLose;
        IReadOnlyReactiveProperty<int> TriesCount { get; }
        void StartGame();
        void Match(List<string> cardIds);
    }
}