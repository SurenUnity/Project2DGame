using Configs.Level;
using UnityEngine;

namespace Configs.SOModels
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Config/Levels", order = 0)]
    public class LevelsSOConfigModel : ScriptableObject
    {
        public LevelsConfigModel levelsConfigModel;
    }
}