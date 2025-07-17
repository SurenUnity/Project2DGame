using Configs.Level;

namespace Services.Level
{
    public class LevelItem : ILevelItem
    {
        private readonly LevelConfigModel _levelConfigModel;

        public int Level => _levelConfigModel.level;
        public int PairCount => _levelConfigModel.pairCount;
        public string[] CardIds => _levelConfigModel.cardIds;
        public int Columns => _levelConfigModel.columns;
        public int Rows => _levelConfigModel.rows;
        public int TriesCount => _levelConfigModel.triesCount;
        
        public LevelItem(LevelConfigModel levelConfigModel)
        {
            _levelConfigModel = levelConfigModel;
        }
    }
}