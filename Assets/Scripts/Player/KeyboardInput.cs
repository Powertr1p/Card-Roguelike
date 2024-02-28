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
            
            if (GameStateGetter.State == TurnState.PlayerPositioningTurn && horizontalInput != 0)
            {
                HandlePositioningStateMovement(horizontalInput);
                _canReceiveInput = true;
                return;
            }

            var desirePosition = GetTargetCardPosition(horizontalInput, verticalInput);
            var target = _raycaster.GetTarget(desirePosition);
            
            _player.StartDragState();
            _player.KeyboardDrag(target);
            _canReceiveInput = true;
        }

        private Vector3 GetTargetCardPosition(float horizontalInput, float verticalInput)
        {
            var movementOffset =
                new Vector3(_deckSpawner.Offset.x * horizontalInput, _deckSpawner.Offset.y * verticalInput, 0f);
            var desirePosition = _player.transform.position + movementOffset;
            return desirePosition;
        }

        private void HandlePositioningStateMovement(float horizontalInput)
        {
            var nextPosition = _player.transform.position + new Vector3(_deckSpawner.Offset.x * horizontalInput, 0f, 0f);

            var hit = _raycaster.GetTarget(_player.transform.position +
                                           new Vector3(_deckSpawner.Offset.x * horizontalInput, _deckSpawner.Offset.y, 0f));

            if (!ReferenceEquals(hit.collider, null) && hit.collider.TryGetComponent(out Placement placement))
            {
                _player.transform.position = nextPosition;
            }
        }
    }
}