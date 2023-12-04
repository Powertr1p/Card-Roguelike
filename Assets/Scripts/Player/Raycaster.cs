using UnityEngine;

namespace DefaultNamespace.Player
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        public RaycastHit2D GetRaycastHit()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z; // Set the z-coordinate based on the camera's position
            Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldMousePosition, Vector2.zero);

            return hit;
        }

        public RaycastHit2D GetBoxcastNearestHit(Transform cardTransform)
        {
            Vector2 boxSize = new Vector2(cardTransform.localScale.x, 0.1f);
            Vector2 boxCenter = new Vector2(cardTransform.position.x, cardTransform.position.y - cardTransform.localScale.y / 2f);
            
            RaycastHit2D[] hits = Physics2D.BoxCastAll(boxCenter, boxSize, 0f, Vector2.down, 0f);
            
            var closestHit = FindClosestHit(cardTransform, hits);

            return closestHit;
        }

        private static RaycastHit2D FindClosestHit(Transform cardTransform, RaycastHit2D[] hits)
        {
            RaycastHit2D closestHit = new RaycastHit2D();
            float closestDistance = float.MaxValue;
            
            foreach (RaycastHit2D hit in hits)
            {
                float distance = Vector2.Distance(cardTransform.position, hit.point);

                if (distance < closestDistance)
                {
                    closestHit = hit;
                    closestDistance = distance;
                }
            }

            return closestHit;
        }
    }
}