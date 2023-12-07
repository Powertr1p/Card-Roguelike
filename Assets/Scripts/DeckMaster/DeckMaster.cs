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

        private List<Card> _deckCards;
        private List<Card> _placements;

        private TurnState _currentState = TurnState.PlayerPositioningTurn;

        private void OnEnable()
        {
            _player.TurnEnded += ConsequencesState;
        }

        private void OnDisable()
        {
            _player.TurnEnded -= ConsequencesState;
        }

        private void Start()
        {
            _deckCards = _spawner.SpawnCards();
            _placements = _spawner.SpawnPlacementsForPlayer();
        }

        private IEnumerator OpenCards(List<Card> cardsToOpen)
        {
            foreach (var card in cardsToOpen)
            {
                //TODO: если карты открыты, то не открывать
                
                card.transform.DORotate(new Vector3(0, 0, 0), 0.25f);

                yield return new WaitForSeconds(0.1f);;
            }
        }

        private List<Card> GetPositionedCard(Vector2 startPosition, Vector2 endPosition)
        {
            var pickedCards = new List<Card>();

            foreach (var card in _deckCards)
            {
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
        private void ConsequencesState(Vector2Int position, Card arg2)
        {
            if (_currentState == TurnState.PlayerPositioningTurn)
            {
                foreach (var placement in _placements)
                {
                    placement.gameObject.SetActive(false);
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
            var cards =  GetPositionedCard(new Vector2(_player.Data.Position.x - 2, _player.Data.Position.y - 2), new Vector2(_player.Data.Position.x + 2, _player.Data.Position.y + 2));
            StartCoroutine(OpenCards(cards));
        }

        private void DeckMasterTurn()
        {
            
        }
    }
}