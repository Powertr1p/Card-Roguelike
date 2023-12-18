using System;
using Cards;
using UnityEngine;

namespace Player
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private PlayerHeroCard _player;
        [SerializeField] private Vector2 _offsetFromPlayer;
        [SerializeField] private float _followDuration;

        private void OnEnable()
        {
            _player.EventTurnEnded += MoveCameraToNewPosition;
        }

        private void OnDisable()
        {
            _player.EventTurnEnded -= MoveCameraToNewPosition;
        }

        private void MoveCameraToNewPosition(Vector2Int vector2Int, Card playerCard)
        {
            
        }
    }
}