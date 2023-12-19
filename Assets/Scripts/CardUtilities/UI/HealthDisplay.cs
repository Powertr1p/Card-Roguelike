using TMPro;
using UnityEngine;

namespace CardUtilities.UI
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _healthLabel;
        [SerializeField] private TextMeshPro _shieldLabel;
        [SerializeField] private Health _health;

        private void OnEnable()
        {
            _health.HealthValueChanged += UpdateHealthVisual;
            _health.ShieldValueChanged += UpdateShieldVisual;
        }

        private void OnDisable()
        {
            _health.HealthValueChanged -= UpdateHealthVisual;
            _health.ShieldValueChanged -= UpdateShieldVisual;
        }

        private void UpdateHealthVisual(int newValue)
        {
            _healthLabel.text = newValue.ToString();
        }

        private void UpdateShieldVisual(int newValue)
        {
            _shieldLabel.text = newValue.ToString();
        }
    }
}