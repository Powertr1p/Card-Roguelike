using UnityEngine;

public class DragAndDropObject : MonoBehaviour
{
   private Camera _camera;
   private Vector3 _mousePosition;

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
      _mousePosition = Input.mousePosition - GetMousePosition();
   }

   private void OnMouseDrag()
   {
      transform.position = _camera.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
   }
}
