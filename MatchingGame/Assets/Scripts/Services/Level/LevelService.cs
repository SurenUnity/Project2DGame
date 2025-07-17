using System.Collections.Generic;
using Configs.Level;
using UniRx;

namespace Services.Level
{
    public class LevelService : ILevelService
    {
        private readonly LevelsConfigModel _levelsConfigModel;
        
        private List<ILevelItem> _levels = new List<ILevelItem>();
        
        private ReactiveProperty<ILevelItem> _levelItem = new();
        private IReactiveProperty<int> _level = new ReactiveProperty<int>(1);

        public IReadOnlyReactiveProperty<int> Level => _level;
        public IReadOnlyReactiveProperty<ILevelItem> LevelItem => _levelItem;
        
        public LevelService(LevelsConfigModel levelsConfigModel)
        {
            _levelsConfigModel = levelsConfigModel;
            
            CreateLevels();
        }

        public void NextLevel()
        {
            _level.Value++;
            if (_level.Value >= _levels.Count)
            {
                _level.Value = _levels.Count;
            }
            
            _levelItem.SetValueAndForceNotify(_levels[_level.Value - 1]);
        }

        private void CreateLevels()
        {
            foreach (var levelConfigModel in _levelsConfigModel.levels)
            {
                var levelItem = new LevelItem(levelConfigModel);
                _levels.Add(levelItem);
            }
        }
    }
}