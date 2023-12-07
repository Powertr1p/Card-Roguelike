using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using DG.Tweening;
using UnityEngine;

namespace DeckMaster
{
    public class DeckMaster : MonoBehaviour
    {
        [SerializeField] private PlayerHeroCard _player;
        [SerializeField] private DeckSpawner _spawner;

        private List<Card> _deckCards;
        private List<Card> _placements;

        private TurnState _currentState = TurnState.PlayerPositioningTurn;

        private void OnEnable()
        {
            _player.TurnEnded += ApplyConsequences;
        }

        private void OnDisable()
        {
            _player.TurnEnded -= ApplyConsequences;
        }

        private void Start()
        {
            _deckCards = _spawner.SpawnCards();
            _placements = _spawner.SpawnPlacementsForPlayer();
            
            //GetPositionedCard(new Vector2(0, 0), new Vector2(6, 0));
            //StartCoroutine(OpenCards());
        }

        private IEnumerator OpenCards(List<Card> cardsToOpen)
        {
            var waitForOneSecond = new WaitForSeconds(0.1f);

            foreach (var card in cardsToOpen)
            {
                card.transform.DORotate(new Vector3(0, 0, 0), 0.25f);

                yield return waitForOneSecond;
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
        
        private void ApplyConsequences(Vector2Int position, Card arg2)
        {
            // if (_currentState == TurnState.PlayerPositioningTurn)
            // {
                foreach (var placement in _placements)
                {
                    placement.gameObject.SetActive(false);
                }
                
                //TODO: брать позицию игрока из даты и плюсовать, проверять открыты ли карты уже или нет и не открытые открывать
                
               var cards =  GetPositionedCard(new Vector2(_player.Data.Position.x - 2, _player.Data.Position.y - 2), new Vector2(_player.Data.Position.x + 2, _player.Data.Position.y + 2));
               StartCoroutine(OpenCards(cards));
               
               ChangeState(TurnState.PlayerTurn);
            //}
               
        }

        private void ChangeState(TurnState nextState)
        {
            _currentState = nextState;
        }
    }
}