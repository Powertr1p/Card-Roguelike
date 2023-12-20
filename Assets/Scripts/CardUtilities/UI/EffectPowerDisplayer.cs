using Cards;
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
            _powerLabel.text = _card.EffectPower.ToString();
        }
    }
}