using System;
using UnityEngine;

namespace UI
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private DeathScreen _screen;

        public event Action AnimationComplete;
        
        private void OnEnable()
        {
            _screen.AnimationComplete += Callback;
        }

        private void OnDisable()
        {  
            _screen.AnimationComplete -= Callback;
        }

        public void Show()
        {
            _screen.Show();
        }

        private void Callback()
        {
            AnimationComplete?.Invoke();
        }
    }
}