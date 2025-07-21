using System;
using System.Collections.Generic;
using Services.Cards;
using Services.Level;
using Services.Score;
using UniRx;
using Views.World.Cards;

namespace Services.MatchingGame
{
    public class MatchingGameService : IMatchingGameService
    {
        public event Action OnGameStarted;
        public event Action OnWin;
        public event Action OnLose;
        public event Action OnLevelQuit;
        
        private readonly ILevelService _levelService;
        private readonly ICardsService _cardsService;
        private readonly IScoreService _scoreService;

        private IReactiveProperty<int> _triesCount = new ReactiveProperty<int>();

        public IReadOnlyReactiveProperty<int> TriesLeftCount => _triesCount;

        public MatchingGameService(ILevelService levelService,
            ICardsService cardsService,
            IScoreService scoreService)
        {
            _levelService = levelService;
            _cardsService = cardsService;
            _scoreService = scoreService;
        }

        public void StartGame()
        {
            _cardsService.PrepareCardsToPlay();
            _triesCount.Value = _levelService.LevelItem.Value.TriesCount;
            OnGameStarted?.Invoke();
        }

        public bool Match(List<int> cardStaticIds)
        {
            var isMatchSuccess = _cardsService.TryMatch(cardStaticIds);
            if (!isMatchSuccess)
            {
                _triesCount.Value--;
                if (_triesCount.Value <= 0)
                {
                    Lose();
                }
                return false;
            }

            _scoreService.IncreaseScore();
            
            var isAllCardsMatched = _cardsService.IsAllCardsMatched();
            if (isAllCardsMatched)
            {
                _scoreService.IncreaseTotalScore();
                Win();
            }

            return true;
        }

        public void QuitLevel()
        {
            OnLevelQuit?.Invoke();
        }

        private void Lose()
        {
            OnLose?.Invoke();
        }
        
        private void Win()
        {
            OnWin?.Invoke();
            _levelService.NextLevel();
        }
    }
}