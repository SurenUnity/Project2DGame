using System.Collections.Generic;
using Configs.Level;
using Cysharp.Threading.Tasks;
using Services.StateSaver;
using States.Level;
using UniRx;

namespace Services.Level
{
    public class LevelService : ILevelService
    {
        private readonly LevelsConfigModel _levelsConfigModel;
        private readonly IStateSaver<LevelStateModel> _stateSaver;

        private LevelStateModel _stateModel;

        private List<ILevelItem> _levels = new();
        
        private ReactiveProperty<ILevelItem> _levelItem = new();
        private IReactiveProperty<int> _level = new ReactiveProperty<int>(1);

        public IReadOnlyReactiveProperty<int> Level => _level;
        public IReadOnlyReactiveProperty<ILevelItem> LevelItem => _levelItem;
        public int LevelsCount => _levels.Count;
        
        public LevelService(LevelsConfigModel levelsConfigModel,
            IStateSaver<LevelStateModel> stateSaver)
        {
            _levelsConfigModel = levelsConfigModel;
            _stateSaver = stateSaver;
        }

        public async UniTask InitAsync()
        {
            await LoadState();
        }

        private async UniTask LoadState()
        {
            _stateModel = await _stateSaver.InitAndLoad();
            CreateLevels();
            SetLevel(_stateModel.Level);
        }

        public void NextLevel()
        {
            _level.Value++;
            SelectLevel();
        }

        public void SetLevel(int level)
        {
            _level.Value = level;
            SelectLevel();
        }

        private void SelectLevel()
        {
            if (_level.Value >= _levels.Count)
            {
                _level.Value = _levels.Count;
            }

            _stateModel.Level = _level.Value;
            
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