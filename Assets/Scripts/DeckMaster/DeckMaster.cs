using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using DeckMaster.StateMachine;
using DefaultNamespace.Player;
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

        private State _currentState;

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
            _currentState = new PlayerPositioningState(_input, _spawner, _deckCards, _player, this);
            _currentState.Process();
        }

        private IEnumerator OpenCards(List<DeckCard> cardsToOpen)
        {
            foreach (var card in cardsToOpen)
            {
                card.OpenCard();

                yield return new WaitForSeconds(0.1f);;
            }
        }

        private List<DeckCard> GetCardsAroundPlayer(Vector2Int startPosition, Vector2Int endPosition, FaceSate skipCondition)
        {
            var pickedCards = new List<DeckCard>();

            foreach (var card in _deckCards)
            {
                if (card.Facing == skipCondition || card.Condition == CardCondition.Dead) continue;

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
            Debug.Log("Player Turn Callback");
            _currentState = _currentState.Process();

            //OpenCardsState();
            //_input.DisableInput();
            //DeckMasterTurn();
            //penCardsState();
            //_input.EnableInput();
        }

        private void OpenCardsState()
        {
            var cards =  GetCardsAroundPlayer(_player.Data.Position - _visibleZone, _player.Data.Position + _visibleZone, FaceSate.FaceUp);
            StartCoroutine(OpenCards(cards));
        }

        private void DeckMasterTurn()
        {
            var nearestCards = GetCardsAroundPlayer(_player.Data.Position - Vector2Int.one, _player.Data.Position + Vector2Int.one, FaceSate.FaceDown);

            foreach (var card in nearestCards)
            {
                Debug.Log(card, card.gameObject);
            }
        }
    }
}