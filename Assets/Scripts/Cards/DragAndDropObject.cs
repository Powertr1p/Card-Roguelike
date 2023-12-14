using DG.Tweening;
using UnityEngine;

namespace Cards
{
   public class DragAndDropObject : MonoBehaviour
   {
      private Camera _camera;
      private Vector3 _mousePosition;
      private Vector3 _initialCardPosition;
      private Transform _transform;

      private void Awake()
      {
         _transform = transform;
         _initialCardPosition = _transform.position;
         _camera = Camera.main;
      }

      private Vector3 GetMousePosition()
      {
         return _camera.WorldToScreenPoint(transform.position);
      }
   
      public void EnterGrabState()
      {
         _mousePosition = Input.mousePosition - GetMousePosition();
         gameObject.layer = 2;
      }

      public void PlaceInitialPosition()
      { 
         _transform.DOMove(_initialCardPosition, 0.5f);
      
         ExitGrabState();
      }

      public void SetNewInitialPosition(Vector3 position)
      {
         _initialCardPosition = new Vector3(position.x, position.y, _initialCardPosition.z);
         _transform.position = _initialCardPosition;
      
         ExitGrabState();
      }

      public void Drag()
      {
         _transform.position = _camera.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
      }

      public void ExitGrabState()
      {
         gameObject.layer = 0;
      }
   }
}
