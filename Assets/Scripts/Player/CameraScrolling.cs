using DeckMaster;
using UnityEngine;

namespace Player
{
    public class CameraScrolling : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Material _backgroundMaterial;

        public bool IsCameraMoving => _isMoving;
        
        private static readonly int TimeValue = Shader.PropertyToID("_TimeValue");
        
        private Transform _cameraTransform;
        private Transform _targetTransform;

        private Vector3 _diffCam;
        private Vector3 _originCam;

        private bool _cameraDrag;
        private bool _isMoving = false;
        private bool _isInstant;

        private void Awake()
        {
            _cameraTransform = _camera.transform;
        }

        private void Update()
        {
            if (_isMoving)
            {
                var cameraPosition = _cameraTransform.position;
                var targetPosition = _targetTransform.position + new Vector3(0f, GameRulesGetter.Rules.OffsetY, 0f);
                
                if (_isInstant)
                {
                    _cameraTransform.position = new Vector3(targetPosition.x, targetPosition.y + GameRulesGetter.Rules.OffsetYOnGameStart, cameraPosition.z);
                    _isInstant = false;
                    _isMoving = false;
                }
                else 
                {
                    var lerpedPosition = Vector3.Lerp(cameraPosition, new Vector3(targetPosition.x, targetPosition.y, cameraPosition.z), Time.deltaTime * GameRulesGetter.Rules.LerpSpeed);
                    transform.position = lerpedPosition;
                    ScrollBackgroundMaterial(lerpedPosition.y);
                }
                
                if (Vector3.Distance(transform.position, new Vector3(targetPosition.x, targetPosition.y, cameraPosition.z)) < GameRulesGetter.Rules.PositionThreshold)
                {
                    _isMoving = false;
                    _isInstant = false;
                }
            }
        }

        public void SetTarget(Transform target, bool isInstant = false)
        {
            _isInstant = isInstant;
            _targetTransform = target;
            _isMoving = true;
        }

        public void StartCameraScrolling()
        {
            if (_isMoving) return;
            
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
                var diff = _originCam - _diffCam;
                _cameraTransform.position = new Vector3(diff.x, diff.y, cameraPosition.z);

                ScrollBackgroundMaterial(diff.y);
            }
        }

        public void StopCameraScrolling()
        {
            _cameraDrag = false;
        }

        public void ListenToScrollMouse()
        {
            if (_isMoving) return;
            
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll == 0) return;
            
            Vector3 newPosition = _cameraTransform.position + Vector3.up * (scroll * GameRulesGetter.Rules.ScrollSpeed);
            _cameraTransform.position = newPosition;
        }

        private void ScrollBackgroundMaterial(float subtracted)
        {
            _backgroundMaterial.SetFloat(TimeValue, subtracted + 10);
        }
    }
}