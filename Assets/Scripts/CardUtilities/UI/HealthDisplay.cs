using System;
using TMPro;
using UnityEngine;

namespace CardUtilities.UI
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _label;
        [SerializeField] private Health _health;

        private void OnEnable()
        {
            _health.HealthValueChanged += UpdateVisual;
        }

        private void OnDisable()
        {
            _health.HealthValueChanged -= UpdateVisual;
        }

        private void UpdateVisual(int newValue)
        {
            _label.text = newValue.ToString();
        }
    }
}