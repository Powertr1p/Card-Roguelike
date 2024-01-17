using Cards;
using DefaultNamespace.Effects.Enums;
using TMPro;
using UnityEngine;

namespace CardUtilities.UI
{
    public class EffectPowerDisplayer : MonoBehaviour
    {
        [SerializeField] private DeckCard _card;
        [SerializeField] private TextMeshPro _powerLabel;

        private void Start()
        {
            if (_card.EffectData.AffectType == AffectType.Instant)
                _powerLabel.text = _card.EffectData.Amount.ToString();
            else if (_card.EffectData.AffectType == AffectType.Turns)
                _powerLabel.text = _card.EffectData.Duration.ToString();
        }
    }
}