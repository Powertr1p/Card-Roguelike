using System;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField, NotNull] private GameObject _mainContainer;
        [SerializeField, NotNull] private Image _mainBackground;
        [SerializeField, NotNull] private Image[] _movingBackgroundPanels;
        [SerializeField, NotNull] private TextMeshProUGUI _deathText;
        [SerializeField] private float _linesAnimationDuration = 0.25f;

        public event Action AnimationComplete;
        
        public void Show()
        {
            PrepareObjects();
            
            StartFillBackground();
            StartLinesAnimation();
            StartPrintingText();
            ChangeFontColor()
                .OnComplete(() =>
                {
                    AnimationComplete?.Invoke();
                });
        }

        private Tween StartFillBackground()
        {
            return _mainBackground.DOFillAmount(1f, _linesAnimationDuration);
        }

        private void PrepareObjects()
        {
            _deathText.alpha = 0f;
            _mainContainer.SetActive(true);
        }

        private void StartLinesAnimation()
        {
            for (int i = 0; i < _movingBackgroundPanels.Length; i++)
            {
                _movingBackgroundPanels[i].DOFillAmount(1f, _linesAnimationDuration).SetDelay(_linesAnimationDuration * 2);
            }
        }
        
        private Tween StartPrintingText()
        {
            return _deathText.DOFade(1f, _linesAnimationDuration).SetDelay(_linesAnimationDuration * 3);
        }

        private Tween ChangeFontColor()
        {
            _deathText.DOFontSize(_deathText.fontSize + 20f, _linesAnimationDuration * 10f).SetDelay(_linesAnimationDuration * 4);
            
            return _deathText.DOColor(Color.red, _linesAnimationDuration * 2f).SetDelay(_linesAnimationDuration * 4);
        }
    }
}

