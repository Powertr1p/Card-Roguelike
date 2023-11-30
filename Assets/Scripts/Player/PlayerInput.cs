using System;
using Cards;
using DefaultNamespace.Interfaces;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Raycaster _raycaster;
        [SerializeField] private CameraScrolling _cameraScrolling;

        public event Action<Vector2> EventCloseUp;
        public event Action<Action> EventReturnCamera;
        
        private bool _isDragging;
        private bool _isCloseUp;
        private bool _hasObject;
        private IDragAndDropable _currentDraggingObject;
        private Vector3 _lastClickedPosition;
        private Vector3 _lastMousePosition;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_isCloseUp)
                {
                    EventReturnCamera?.Invoke(() => { _isCloseUp = false;});
                    return;
                }
                
                RaycastHit2D hit = _raycaster.GetRaycastHit();
                
                _lastClickedPosition = Input.mousePosition;
                
                if (hit)
                {
                    if (hit.collider.TryGetComponent(out IDragAndDropable dndObject))
                    {
                        StartGrabState(dndObject);
                    }
                    else if (hit.collider.TryGetComponent(out Card card))
                    {
                        PerformCloseUp(card.transform.position);
                    }
                }
            }
            
            if (!_hasObject) return;
            
            if (Input.GetMouseButton(0) && _lastClickedPosition != _lastMousePosition)
            {
                PerformDragging();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_lastClickedPosition == _lastMousePosition)
                {
                    PerformCloseUp(_currentDraggingObject.GetPosition());
                }
                
                if (_isDragging)
                {
                    _currentDraggingObject.TryPlaceSelf();
                    
                    DisableDraggingState();
                }
            }
            
            _lastMousePosition = Input.mousePosition;
        }
        
        private void LateUpdate()
        {
            if (!_hasObject && !_isCloseUp)
            {
                _cameraScrolling.ListenToScrollMouse();

                if (Input.GetMouseButton(0))
                {
                    _cameraScrolling.StartCameraScrolling();
                }
                else
                {
                    _cameraScrolling.StopCameraScrolling();
                }
            }
        }

        private void StartGrabState(IDragAndDropable dndObject)
        {
            _currentDraggingObject = dndObject;
            dndObject.Grab();
            _hasObject = true;
        }

        private void DisableDraggingState()
        {
            _isDragging = false;
            _currentDraggingObject = null;
            _hasObject = false;
        }

        private void ConsumeCard(Card card)
        {
            Vector3 newPosition = card.transform.position;
            _currentDraggingObject.SetNewInitialPosition(newPosition);

            Destroy(card.gameObject);
        }

        private void PerformCloseUp(Vector3 position)
        {
            EventCloseUp?.Invoke(position);
            _isCloseUp = true;
        }

        private void PerformDragging()
        {
            _isDragging = true;
            _currentDraggingObject.Drag();
        }
    }
}