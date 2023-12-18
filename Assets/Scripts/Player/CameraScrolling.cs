using DefaultNamespace.Player;
using UnityEngine;

namespace Player
{
    public class CameraScrolling : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Material _backgroundMaterial;
        [SerializeField] private PlayerInput _input;

        private static readonly int TimeValue = Shader.PropertyToID("_TimeValue");
        
        private Transform _cameraTransform;

        private Vector3 _diffCam;
        private Vector3 _originCam;

        public float _scrollSpeed = 2.0f;
        
        private bool _cameraDrag;
        
        public Transform _targetTransform; 
        public float _lerpSpeed = 3.0f; 
        public float _positionThreshold = 0.1f; 

        private bool _isMoving = false;

        private void Awake()
        {
            _cameraTransform = _camera.transform;
        }
        
        private void Update()
        {
            if (_isMoving)
            {
                var cameraPosition = transform.position;
                var targetPosition = _targetTransform.position;

                var lerpedPosition = Vector3.Lerp(cameraPosition, new Vector3(cameraPosition.x, targetPosition.y, cameraPosition.z), Time.deltaTime * _lerpSpeed);

                transform.position = lerpedPosition;
                
                ScrollBackgroundMaterial(lerpedPosition.y);
                
                if (Vector3.Distance(transform.position, new Vector3(cameraPosition.x, targetPosition.y, cameraPosition.z)) < _positionThreshold)
                {
                    _isMoving = false; 
                }
            }
        }

        public void SetTarget(Transform target)
        {
            _targetTransform = target;
            _isMoving = true;
        }

        public void StartCameraScrolling()
        {
            var cameraPosition = _cameraTransform.position;
            _diffCam = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraPosition.z * -1f)) - cameraPosition;

            if (!_cameraDrag)
            {
                _cameraDrag = true;
                _originCam = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraPosition.z * -1f));

                ScrollBackgroundMaterial(_originCam.y - _diffCam.y);
            }

            if (_cameraDrag)
            {
                var diff = _originCam.y - _diffCam.y;
                _cameraTransform.position = new Vector3(cameraPosition.x, diff, cameraPosition.z);

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
            _backgroundMaterial.SetFloat(TimeValue, subtracted + 10);
        }
    }
}