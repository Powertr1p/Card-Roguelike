using System;
using Cards;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Raycaster _raycaster;

        public event Action<Vector2> EventCloseUp;
        public event Action<Action> EventReturnCamera;
        
        private bool _isDragging;
        private bool _isCloseUp;
        private bool _hasObject;
        private DragAndDropObject _currentDraggingObject;
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
                
                RaycastHit2D hit = _raycaster.GetRaycastHits();
                
                _lastClickedPosition = Input.mousePosition;
                
                if (hit)
                {
                    if (hit.collider.TryGetComponent(out DragAndDropObject dndObject))
                    {
                        _currentDraggingObject = dndObject;
                        dndObject.Grab();
                        _hasObject = true;
                    }
                    else if (hit.collider.TryGetComponent(out Card card))
                    {
                        EventCloseUp?.Invoke(card.transform.position);
                        _isCloseUp = true;
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
                    EventCloseUp?.Invoke(_currentDraggingObject.transform.position);
                    _isCloseUp = true;
                }
                
                if (_isDragging)
                {
                    RaycastHit2D hit = _raycaster.GetRaycastHits();

                    if (!ReferenceEquals(hit.collider,null))
                    {
                        if (!hit.collider.TryGetComponent(out Card card)) return;
                        
                        Vector3 newPosition = card.transform.position;
                        _currentDraggingObject.SetNewInitialPosition(newPosition);
                        Destroy(card.gameObject);
                    }
                    else
                    {
                        _currentDraggingObject.PlaceInitialPosition();
                    }
                    
                    _isDragging = false;
                    _currentDraggingObject = null;
                    _hasObject = false;
                }
            }

            _lastMousePosition = Input.mousePosition;
        }

        private void PerformDragging()
        {
            _isDragging = true;
            _currentDraggingObject.Drag();
        }
    }
}