using Configs.Cards;
using UnityEngine;

namespace Configs.SOModels
{
    [CreateAssetMenu(fileName = "CardsConfig", menuName = "Config/Cards", order = 0)]
    public class CardsSOConfigModel : ScriptableObject
    {
        public CardsConfigModel cardsConfig;
    }
}