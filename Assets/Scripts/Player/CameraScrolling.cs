using UnityEngine;

namespace Player
{
    public class CameraScrolling : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Material _backgroundMaterial;

        private Transform _cameraTransform;

        private Vector3 _diffCam;
        private Vector3 _originCam;
        
        public float _scrollSpeed = 2.0f;
        
        private bool _cameraDrag;

        private void Awake()
        {
            ScrollBackgroundMaterial(_originCam.y, _diffCam.y);
            _cameraTransform = _camera.transform;
        }

        public void StartCameraScrolling()
        {
            _diffCam = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cameraTransform.position.z * -1f)) - _cameraTransform.position;

            if (!_cameraDrag)
            {
                _cameraDrag = true;
                _originCam = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cameraTransform.position.z * -1f));
            }

            if (_cameraDrag)
            {
                var position = _cameraTransform.position;
                var diff = _originCam.y - _diffCam.y;
                _cameraTransform.position = new Vector3(position.x, diff, position.z);
                ScrollBackgroundMaterial(diff);
            }
        }

        public void StopCameraScrolling()
        {
            _cameraDrag = false;
        }

        public void ListenToScrollMouse()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll == 0) return;
            
            Vector3 newPosition = _cameraTransform.position + Vector3.up * (scroll * _scrollSpeed);
            _cameraTransform.position = newPosition;
        }
        private void ScrollBackgroundMaterial(float subtracted)
        {
            _backgroundMaterial.SetFloat("_TimeValue", subtracted + 10);
        }

        private void ScrollBackgroundMaterial(float origin, float difference)
        {
            var subtracted = origin - difference;
            _backgroundMaterial.SetFloat("_TimeValue", subtracted + 10);
        }
    }
}