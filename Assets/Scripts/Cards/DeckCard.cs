using DG.Tweening;
using UnityEngine;

namespace Cards
{
    public abstract class DeckCard : Card
    {
        private Transform _transform;

        public FaceSate Facing => _facing;
        private FaceSate _facing = FaceSate.FaceDown;

        private void Awake()
        {
            _transform = transform;
        }

        public void OpenCard()
        {
            _transform.DORotate(Vector3.zero, 0.25f).OnComplete(() =>
            {
                _facing = FaceSate.FaceUp;
            });
        }
    }
}