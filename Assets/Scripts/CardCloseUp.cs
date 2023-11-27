using System;
using DefaultNamespace.Player;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class CardCloseUp : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerInput _input;

        private Vector3 _initialPosition;
        private bool _isPlaying;
        
        private void Awake()
        {
            _initialPosition = _camera.transform.position;
        }

        private void OnEnable()
        {
            _input.EventCloseUp += CloseUpCard;
            _input.EventReturnCamera += EventReturnCamera;
        }

        private void OnDisable()
        {
            _input.EventCloseUp -= CloseUpCard;
            _input.EventReturnCamera -= EventReturnCamera;
        }

        private void CloseUpCard(Vector2 position)
        {
            if (_isPlaying) return;
            _isPlaying = true;
            
            var targetPosition = new Vector3(position.x, position.y, _initialPosition.z);
            
            _camera.transform.DOMove(targetPosition, 0.5f);
            _camera.DOOrthoSize(1.1f, 0.5f).OnComplete(() =>
            {
                _isPlaying = false;
            });
        }
        
        private void EventReturnCamera(Action onCompleteCallback)
        {
            //переделать в секвенцию и проверять пока играется
            if (_isPlaying) return;
            _isPlaying = true;
            
            _camera.transform.DOMove(_initialPosition, 0.5f);
            _camera.DOOrthoSize(5f, 0.5f).OnComplete(() =>
            {
                _isPlaying = false;
                onCompleteCallback?.Invoke();
            });
        }
    }
}