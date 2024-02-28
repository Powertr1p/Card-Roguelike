using System;
using Cards;
using DeckMaster;
using DefaultNamespace.Interfaces;
using Player;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Raycaster _raycaster;
        [SerializeField] private CameraScrolling _cameraScrolling;

        public event Action<Vector2> EventCloseUp;
        public event Action<Action> EventReturnCamera;
        public event Action<float, float> MovePressed;
        
        private bool _isDragging;
        private bool _isCloseUp;
        private bool _hasDraggingObject;
        private bool _isInputEnabled = true;
        private IDragAndDropable _currentDraggingObject;
        private Vector3 _lastClickedPosition;
        private Vector3 _lastMousePosition;
        private float _lastVerticalInput;
        private float _lastHorizontalInput;

        private void Update()
        {
            if (!_isInputEnabled) return;
            if (_cameraScrolling.IsCameraMoving) return;

            TryHandleMouseInput();
            TryHandleKeyboardInput();
        }

        private void LateUpdate()
        {
            if (!_isInputEnabled) return;
            if (_cameraScrolling.IsCameraMoving) return;
            
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
        
        private void TryHandleMouseInput()
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

        private void TryHandleKeyboardInput()
        {
            if (IsGetKeyDown())
            {
                float horizontalInput = Input.GetAxisRaw("Horizontal");
                float verticalInput = Input.GetAxisRaw("Vertical");

                if (horizontalInput != 0 || verticalInput != 0)
                {
                    _lastVerticalInput = verticalInput;
                    _lastHorizontalInput = horizontalInput;
                }
            }
            
            if (IsGetKeyUp())
            {
                MovePressed?.Invoke(_lastHorizontalInput, _lastVerticalInput);
            }
        }

        private bool IsGetKeyUp()
        {
            return Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow);
        }

        private bool IsGetKeyDown()
        {
            return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow);
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