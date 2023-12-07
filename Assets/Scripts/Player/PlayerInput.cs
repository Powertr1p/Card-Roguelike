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
        private bool _hasDraggingObject;
        private bool _isInputEnabled = true;
        private IDragAndDropable _currentDraggingObject;
        private Vector3 _lastClickedPosition;
        private Vector3 _lastMousePosition;

        private void Update()
        {
            if (!_isInputEnabled) return;

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
                        Grab(dndObject);
                    }
                }
            }
            
            if (Input.GetMouseButton(0) && _lastClickedPosition != _lastMousePosition)
            {
                if (!_hasDraggingObject) return;
                
                PerformDragging();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_hasDraggingObject)
                {
                    DisableDragging();
                }
                
                if (_lastClickedPosition == _lastMousePosition && !_isCloseUp && !_isDragging)
                {
                    TryPerformCloseUp();
                }
            }
            
            _lastMousePosition = Input.mousePosition;
        }

        private void LateUpdate()
        {
            if (!_hasDraggingObject)
            {
                _cameraScrolling.ListenToScrollMouse();

                if (Input.GetMouseButton(0))
                {
                    if (!_isCloseUp)
                        _cameraScrolling.StartCameraScrolling();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    _cameraScrolling.StopCameraScrolling();
                }
            }
        }
        
        public void DisableInput()
        {
            _isInputEnabled = false;
        }

        public void EnableInput()
        {
            _isInputEnabled = true;
        }

        private void Grab(IDragAndDropable dndObject)
        {
            _currentDraggingObject = dndObject;
            dndObject.StartDragState();
            _hasDraggingObject = true;
        }
        
        private void TryPerformCloseUp()
        {
            RaycastHit2D hit = _raycaster.GetRaycastHit();

            if (hit)
            {
                if (hit.collider.TryGetComponent(out Card card))
                {
                    PerformCloseUp(card.transform.position);
                }
            }
        }

        private void DisableDragging()
        {
            _currentDraggingObject.ExitDragState();

            _isDragging = false;
            _currentDraggingObject = null;
            _hasDraggingObject = false;
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