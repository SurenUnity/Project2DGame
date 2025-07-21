using UnityEngine;

namespace Views.World.Cards
{
    [CreateAssetMenu(fileName = "CardAnimationSettings", menuName = "Animation/Card", order = 0)]
    public class CardAnimationSettings : ScriptableObject
    {
        public float selectScaleValue;
        public float selectDuration;
        public float deselectScaleValue;
        public float deselectDuration;
        public float matchScaleValue;
        public float matchDuration;
    }
}