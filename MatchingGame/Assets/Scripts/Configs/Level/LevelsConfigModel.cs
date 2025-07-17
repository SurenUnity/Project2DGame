using System;

namespace Configs.Level
{
    [Serializable]
    public class LevelsConfigModel
    {
        public LevelConfigModel[] levels;
    }

    [Serializable]
    public class LevelConfigModel
    {
        public int level;
        public int pairCount;
        public string[] cardIds;
        public int columns;
        public int rows;
        public int triesCount;
    }
}