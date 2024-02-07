using TMPro;
using UnityEngine;

namespace UI
{
    public class KillsDisplay : MonoBehaviour
    {
        [SerializeField] private DeckMaster.DeckMaster _master;
        [SerializeField] private TextMeshProUGUI _killsText;

        private int _killCount;

        private void Awake()
        {
            _killCount = 0;
            UpdateText();
        }

        private void OnEnable()
        {
            _master.EnemyDeath += UpdateVisuals;
        }

        private void OnDisable()
        {
            _master.EnemyDeath -= UpdateVisuals;
        }

        private void UpdateVisuals()
        {
            IncrementKills();
            UpdateText();
        }

        private void UpdateText()
        {
            _killsText.text = _killCount.ToString();
        }

        private void IncrementKills()
        {
            _killCount++;
        }
    }
}