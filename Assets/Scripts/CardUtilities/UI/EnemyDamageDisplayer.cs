using Cards;
using TMPro;
using UnityEngine;

namespace CardUtilities.UI
{
    public class EnemyDamageDisplayer : MonoBehaviour
    {
        [SerializeField] private EnemyCard _card;
        [SerializeField] private TextMeshPro _powerDisplay;

        private void Start()
        {
            _powerDisplay.text = _card.SummDamage.ToString();
        }
    }
}