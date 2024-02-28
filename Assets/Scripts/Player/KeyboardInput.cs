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
                //TODO: дать походить под позишионами, пока не нажмет вверх
                //TODO: не давать ходить вниз
            }
            
            Debug.Log("V: " + horizontalInput);
            Debug.Log("H: " +verticalInput);
            
            var desirePosition = _player.transform.position + new Vector3(1.6f * horizontalInput, 2.2f * verticalInput, 0f);
            
            var target = _raycaster.GetTarget(desirePosition);
            
            _player.StartDragState();
            _player.KeyboardDrag(target);
            _canReceiveInput = true;
        }
    }
}