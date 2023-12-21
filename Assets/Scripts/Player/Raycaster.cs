using UnityEngine;

namespace Player
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Collider2D _collider;
        
        public RaycastHit2D GetRaycastHit()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -_camera.transform.position.z; // Set the z-coordinate based on the camera's position
            Vector2 worldMousePosition = _camera.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldMousePosition, Vector2.zero);

            return hit;
        }

        public RaycastHit2D GetBoxcastNearestHit(Transform cardTransform)
        {
            Transform t = cardTransform;
            Vector3 scale = t.localScale;
            Bounds bounds = _collider.bounds;

            Vector2 boxSize = new Vector2(scale.x, bounds.size.y / 1.2f);
            Vector2 boxCenter = bounds.center;
            
            RaycastHit2D[] hits = Physics2D.BoxCastAll(boxCenter, boxSize, 0f, Vector2.down, 0f);
            DebugDrawBoxCast(boxCenter, boxSize, Color.red, Vector2.up * 0.01f);
            
            var closestHit = FindClosestHit(t, hits);

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
        
        private void DebugDrawBoxCast(Vector2 center, Vector2 size, Color color, Vector2 verticalOffset)
        {
            Vector2 topLeft = center - size * 0.5f;
            Vector2 topRight = center + new Vector2(size.x * 0.5f, -size.y * 0.5f);
            Vector2 bottomLeft = center + new Vector2(-size.x * 0.5f, size.y * 0.5f);
            Vector2 bottomRight = center + size * 0.5f;

            topLeft += verticalOffset;
            topRight += verticalOffset;
            bottomLeft += verticalOffset;
            bottomRight += verticalOffset;
            
            Debug.DrawLine(topLeft, topRight, color);
            Debug.DrawLine(topRight, bottomRight, color);
            Debug.DrawLine(bottomRight, bottomLeft, color);
            Debug.DrawLine(bottomLeft, topLeft, color);
        }
    }
}