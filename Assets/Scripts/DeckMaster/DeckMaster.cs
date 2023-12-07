using System.Collections;
using System.Collections.Generic;
using Cards;
using DG.Tweening;
using UnityEngine;

namespace DeckMaster
{
    public class DeckMaster : MonoBehaviour
    {
        [SerializeField] private HeroCard _player;
        [SerializeField] private DeckSpawner _spawner;

        private List<Card> _deckCards;
        private List<Card> _placements;

        private void Start()
        {
            _deckCards = _spawner.SpawnCards();
            _placements = _spawner.SpawnPlacementsForPlayer();
            
            GetPositionedCard(new Vector2(0, 0), new Vector2(6, 0));

            //StartCoroutine(OpenCards());
        }

        private IEnumerator OpenCards()
        {
            var waitForOneSecond = new WaitForSeconds(0.1f);

            foreach (var card in _deckCards)
            {
                card.transform.DORotate(new Vector3(0, 0, 0), 0.25f);

                yield return waitForOneSecond;
            }
        }

        public List<Card> GetPositionedCard(Vector2 startPosition, Vector2 endPosition)
        {
            var pickedCards = new List<Card>();

            foreach (var card in _deckCards)
            {
                if (card.Data.Position.y >= startPosition.y && card.Data.Position.y <= endPosition.y)
                {
                    if (card.Data.Position.x >= startPosition.x && card.Data.Position.x <= endPosition.x)
                    {
                        Debug.Log(card.Data.Position, card.gameObject);
                    }
                }
            }

            return pickedCards;
        }
    }
}