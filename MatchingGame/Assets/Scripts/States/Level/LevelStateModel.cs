using System;
using Services.StateSaver;

namespace States.Level
{
    [Serializable]
    public class LevelStateModel : ISaveData
    {
        private int _level;

        public int Level
        {
            get => _level;
            set => _level = value;
        }
    }
}