using System;
using UnityEngine;

public class DragAndDropObject : MonoBehaviour
{
   private Camera _camera;
   private Vector3 _mousePosition;
   private Vector3 _lastMouseUpPosition;
   private Vector3 _lastMouseDownPosition;

   private float _delta;
   private bool _isDrag;
   private bool _isDown;

   private void Awake()
   {
      _camera = Camera.main;
   }

   private Vector3 GetMousePosition()
   {
      return _camera.WorldToScreenPoint(transform.position);
   }

   private void OnMouseDown()
   {
      _isDown = true;
      _mousePosition = Input.mousePosition - GetMousePosition();
      _lastMouseDownPosition = Input.mousePosition;
   }

   private void OnMouseUp()
   {
      _lastMouseUpPosition = Input.mousePosition;

      if (_isDown && _lastMouseUpPosition == _lastMouseDownPosition)
      {
         Debug.Log("SHOW CARD");
      }
      
      _isDrag = false;
      _isDown = false;
   }

   private void OnMouseDrag()
   {
      _isDrag = true;

      transform.position = _camera.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
   }
}
