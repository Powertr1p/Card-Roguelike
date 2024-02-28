using Cards;
using DeckMaster;
using DefaultNamespace.Player;
using UnityEngine;

namespace Player
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private PlayerHeroCard _player;
        [SerializeField] private Raycaster _raycaster;
        [SerializeField] private DeckSpawner _deckSpawner;

        private bool _canReceiveInput = true;

        private void OnEnable()
        {
            _input.MovePressed += HandleMovement;
        }

        private void OnDisable()
        {
            _input.MovePressed -= HandleMovement;
        }
        
        private void HandleMovement(float horizontalInput, float verticalInput)
        {
            if (!_canReceiveInput) return;
            _canReceiveInput = false;

            if (GameStateGetter.State == TurnState.PlayerPositioningTurn)
            {
                if (horizontalInput != 0 && verticalInput == 0)
                {
                    HandlePositioningStateMovement(horizontalInput);
                    _canReceiveInput = true;
                    return;
                }
            }
            
            var desirePosition = GetTargetCardPosition(horizontalInput, verticalInput);
            var target = _raycaster.GetTarget(desirePosition);
            
            _player.StartDragState();
            _player.transform.position = desirePosition;
            _player.KeyboardDrag(target);
            _canReceiveInput = true;
        }

        private Vector3 GetTargetCardPosition(float horizontalInput, float verticalInput)
        {
            Vector3 movementOffset = new Vector3(_deckSpawner.Offset.x * horizontalInput, _deckSpawner.Offset.y * verticalInput, 0f);
            Vector3 desirePosition = _player.transform.position + movementOffset;
            
            return desirePosition;
        }

        private void HandlePositioningStateMovement(float horizontalInput)
        {
            Vector3 playerPosition = _player.transform.position;
            Vector3 nextPosition = playerPosition + new Vector3(_deckSpawner.Offset.x * horizontalInput, 0f, 0f);
            
            if (IsPlacementAbovePosition(nextPosition))
            {
                _player.transform.position = nextPosition;
            }
        }

        private bool IsPlacementAbovePosition(Vector3 position)
        {
            var hit = _raycaster.GetTarget(position + new Vector3(0f, _deckSpawner.Offset.y, 0f));

            return !ReferenceEquals(hit.collider, null) && hit.collider.TryGetComponent(out Placement placement);
        }
    }
}