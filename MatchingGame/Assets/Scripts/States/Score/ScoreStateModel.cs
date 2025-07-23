using System;
using Services.StateSaver;

namespace States.Score
{
    [Serializable]
    public class ScoreStateModel : ISaveData
    {
        private int _totalScore;
        
        public int TotalScore
        {
            get => _totalScore;
            set => _totalScore = value;
        }
    }
}