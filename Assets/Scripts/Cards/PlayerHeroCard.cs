using DefaultNamespace.Interfaces;
using DefaultNamespace.Player;
using UnityEngine;

namespace Cards
{
    public class PlayerHeroCard : HeroCard, IDragAndDropable
    {
        [SerializeField] private DragAndDropObject _dragBehaviour;
        [SerializeField] private Raycaster _raycaster;
        
        public override void Interact(HeroCard interactorCard)
        {
            //сюда падает обычная карта
            //спрашиваем
        }

        public void Grab()
        {
            _dragBehaviour.Grab();
        }

        public void PlaceInitialPosition()
        {
            _dragBehaviour.PlaceInitialPosition();
        }

        public void SetNewInitialPosition(Vector3 position)
        {
            _dragBehaviour.SetNewInitialPosition(position);
        }

        public void Drag()
        {
            _dragBehaviour.Drag();
        }

        public Vector3 GetPosition()
        {
            return _dragBehaviour.GetPosition();
        }

        public void TryPlace()
        {
            var hit = _raycaster.GetBoxcastNearestHit(transform);

            if (hit)
            {
                SetNewInitialPosition(hit.collider.transform.position);
            }
            else
            {
                PlaceInitialPosition();
            }
        }
    }
}