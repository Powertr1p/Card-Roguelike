using UnityEngine;

namespace DefaultNamespace.Player
{
    public class CameraScrolling : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private Transform _cameraTransform;
        
        private Vector3 _diffCam;
        private Vector3 _originCam;
        
        private bool _cameraDrag;

        private void Awake()
        {
            _cameraTransform = _camera.transform;
        }

        public void PerformScroll()
        {
            _diffCam = _camera.ScreenToWorldPoint(Input.mousePosition) - _camera.transform.position;

            if (!_cameraDrag)
            {
                _cameraDrag = true;
                _originCam = _camera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (_cameraDrag)
            {
                var position = _cameraTransform.position;
                _cameraTransform.position = new Vector3(position.x, _originCam.y - _diffCam.y, position.z);
            }
        }

        public void ExitScroll()
        {
            _cameraDrag = false;
        }
    }
}