using UnityEngine;

namespace DefaultNamespace.Interfaces
{
    public interface IDragAndDropable
    {
        public void StartDragState();
        public void Drag();
        public void ExitDragState();
    }
}