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
        [SerializeField, NotNull] private Image _leftBgPanel;
        [SerializeField, NotNull] private Image _rightBgPanel;
        [SerializeField, NotNull] private TextMeshProUGUI _deathText;
        [SerializeField] private float _linesAnimationDuration = 0.25f;

        public event Action AnimationComplete;

        private Sequence _animationSequence;

        public void Show()
        {
            PrepareObjects();
            BuildSequence();
            StartAnimationSequence();
        }

        private Tween StartFillBackground(float duration)
        {
            return _mainBackground.DOFillAmount(1f, duration);
        }

        private void PrepareObjects()
        {
            _deathText.alpha = 0f;
            _mainContainer.SetActive(true);
        }

        private void BuildSequence()
        {
            _animationSequence = DOTween.Sequence().SetAutoKill(false);

            _animationSequence
                .Append(StartFillBackground(0.25f))
                .Append(MovePanels(_leftBgPanel, 0.25f))
                .Append(StartPrintingText(0.25f))
                .Insert(0.25f, MovePanels(_rightBgPanel, 0.25f))
                .Insert(0.75f, ChangeTextColor(0.25f))
                .Insert(0f, ChangeFontSize(_animationSequence.Duration() + 2f))
                .InsertCallback(1f, OnCompleteCallback);
        }

        private void StartAnimationSequence()
        {
            _animationSequence.Play();
        }

        private Tween MovePanels(Image panel, float duration)
        {
            return panel.DOFillAmount(1f, duration);
        }
        
        private Tween StartPrintingText(float duration)
        {
            return _deathText.DOFade(1f, duration);
        }

        private Tween ChangeFontSize(float duration)
        {
            return _deathText.DOFontSize(_deathText.fontSize + 20f, duration);
        }

        private Tween ChangeTextColor(float duration)
        {
            return _deathText.DOColor(Color.red, duration);
        }

        private void OnCompleteCallback()
        {
            AnimationComplete?.Invoke();
            _animationSequence.Kill();
        }
    }
}

