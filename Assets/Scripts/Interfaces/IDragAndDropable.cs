using UnityEngine;

namespace DefaultNamespace.Interfaces
{
    public interface IDragAndDropable
    {
        public void Grab();
        public void PlaceInitialPosition();
        public void SetNewInitialPosition(Vector3 position);
        public void Drag();
        public Vector3 GetPosition();
        public void TryPlace();
    }
}