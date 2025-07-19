using UnityEngine;

namespace Configs.SOModels
{
    [CreateAssetMenu(fileName = "GlobalConfig", menuName = "Config/Global", order = 0)]
    public class GlobalConfigSO : ScriptableObject
    {
        public CardsSOConfigModel cardsSoConfigModel;
        public LevelsSOConfigModel levelsSoConfigModel;
    }
}