using UnityEngine;

namespace DefaultNamespace.Player
{
    public class CameraScrolling : MonoBehaviour
    {
        private Vector3 _diffCam;
        private bool _cameraDrag;
        private Vector3 _originCam;

        public void PerformScroll()
        {
            Debug.Log("Sss");

            _diffCam = Camera.main.ScreenToWorldPoint(Input.mousePosition) -
                       Camera.main.transform.position;

            if (!_cameraDrag)
            {
                _cameraDrag = true;
                _originCam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (_cameraDrag)
            {
                var pos = Camera.main.transform.position;
                Camera.main.transform.position = new Vector3(pos.x, _originCam.y - _diffCam.y, pos.z);
            }
        }

        public void ExitScroll()
        {
            _cameraDrag = false;
        }
    }
}