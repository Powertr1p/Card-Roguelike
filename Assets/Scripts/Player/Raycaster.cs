using UnityEngine;

namespace DefaultNamespace.Player
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        public RaycastHit2D GetRaycastHits()
        {
            Vector2 origin = _camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero);

            return hit;
        }
    }
}