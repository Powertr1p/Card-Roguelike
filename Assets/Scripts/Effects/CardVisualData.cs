using UnityEngine;

namespace DefaultNamespace.Effects
{
    [CreateAssetMenu(fileName = "VisualData", menuName = "VisualData", order = 0)]
    public class CardVisualData : ScriptableObject
    {
        [SerializeField] private Sprite _iconSprite;
        [SerializeField] private Sprite _frameSprite;

        public Sprite Icon => _iconSprite;
        public Sprite Frame => _frameSprite;
    }
}