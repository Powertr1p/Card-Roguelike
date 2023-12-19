using UnityEngine;

namespace DefaultNamespace.Effects
{
    [CreateAssetMenu(fileName = "VisualData", menuName = "VisualData", order = 0)]
    public class CardVisualData : ScriptableObject
    {
        [SerializeField] private Sprite _iconSprite;

        public Sprite Icon => _iconSprite;
    }
}