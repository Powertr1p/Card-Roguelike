using DG.Tweening;
using UnityEngine;

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
   
   public void Grab()
   {
      _mousePosition = Input.mousePosition - GetMousePosition();
      gameObject.layer = 2;
   }

   public void PlaceInitialPosition()
   { 
      _transform.DOMove(_initialCardPosition, 0.5f);
      
      gameObject.layer = 0;
   }

   public void SetNewInitialPosition(Vector3 position)
   {
      _initialCardPosition = position;
      _transform.position = _initialCardPosition;
      gameObject.layer = 0;
   }

   public void Drag()
   {
      _transform.position = _camera.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
   }

   public Vector3 GetPosition()
   {
      return _transform.position;
   }
}
