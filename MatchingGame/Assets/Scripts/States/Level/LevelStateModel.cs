using System;
using Services.StateSaver;

namespace States.Level
{
    [Serializable]
    public class LevelStateModel : ISaveData
    {
        private int _level = 1;

        public int Level
        {
            get => _level;
            set => _level = value <= 0 ? 1 : value;
        }
    }
}