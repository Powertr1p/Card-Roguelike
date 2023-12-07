using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using DefaultNamespace.Player;
using DG.Tweening;
using UnityEngine;

namespace DeckMaster
{
    public class DeckMaster : MonoBehaviour
    {
        [SerializeField] private PlayerHeroCard _player;
        [SerializeField] private PlayerInput _input;
        [SerializeField] private DeckSpawner _spawner;
        [SerializeField] private Vector2Int _visibleZone = new Vector2Int(2,2);

        private List<DeckCard> _deckCards;
        private List<Card> _placements;

        private TurnState _currentState = TurnState.PlayerPositioningTurn;

        private void OnEnable()
        {
            _player.EventTurnEnded += OnPlayerTurnEnded;
        }

        private void OnDisable()
        {
            _player.EventTurnEnded -= OnPlayerTurnEnded;
        }

        private void Start()
        {
            _deckCards = _spawner.SpawnCards();
            _placements = _spawner.SpawnPlacementsForPlayer();
        }

        private IEnumerator OpenCards(List<DeckCard> cardsToOpen)
        {
            foreach (var card in cardsToOpen)
            {
                card.OpenCard();

                yield return new WaitForSeconds(0.1f);;
            }
        }

        private List<DeckCard> GetCardsToOpen(Vector2Int startPosition, Vector2Int endPosition)
        {
            var pickedCards = new List<DeckCard>();

            foreach (var card in _deckCards)
            {
                if (card.Facing == FaceSate.FaceUp) continue;
                
                if (card.Data.Position.y >= startPosition.y && card.Data.Position.y <= endPosition.y)
                {
                    if (card.Data.Position.x >= startPosition.x && card.Data.Position.x <= endPosition.x)
                    {
                        pickedCards.Add(card);
                    }
                }
            }

            return pickedCards;
        }
        
        //TODO: отрефакторить в стейт-машину
        private void OnPlayerTurnEnded(Vector2Int position, Card arg2)
        {
            if (_currentState == TurnState.PlayerPositioningTurn)
            {
                foreach (var placement in _placements)
                {
                    Destroy(placement.gameObject);
                }
                
                OpenCardsState();
                ChangeState(TurnState.PlayerTurn);
                return;
            }

            ChangeState(TurnState.DeckMasterTurn);
            _input.DisableInput();
            
            DeckMasterTurn();
            OpenCardsState();
            
            ChangeState(TurnState.PlayerTurn);
            _input.EnableInput();
        }

        private void ChangeState(TurnState nextState)
        {
            _currentState = nextState;
        }

        private void OpenCardsState()
        {
            var cards =  GetCardsToOpen(_player.Data.Position - _visibleZone, _player.Data.Position + _visibleZone);
            StartCoroutine(OpenCards(cards));
        }

        private void DeckMasterTurn()
        {
            
        }
    }
}