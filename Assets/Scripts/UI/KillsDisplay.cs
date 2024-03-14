using Data;
using TMPro;
using UnityEngine;

namespace UI
{
    public class KillsDisplay : MonoBehaviour
    {
        [SerializeField] private DeckMaster.DeckMaster _master;
        [SerializeField] private TextMeshProUGUI _killsText;
        
        private void OnEnable()
        {
            _master.EnemyDeath += UpdateVisuals;
        }

        private void OnDisable()
        {
            _master.EnemyDeath -= UpdateVisuals;
        }
        
        private void Start()
        {
            UpdateText();
        }

        private void UpdateVisuals()
        {
            IncrementKills();
            UpdateText();
        }

        private void UpdateText()
        {
            _killsText.text = PlayerStatsStorage.Kills.ToString();
        }

        private void IncrementKills()
        {
            PlayerStatsStorage.Kills++;
        }
    }
}