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
        
        private void Start()
        {
            _deckCards = _spawner.SpawnCards();

            StartCoroutine(OpenCards());
        }

        private IEnumerator OpenCards()
        {
            var waitForOneSecond = new WaitForSeconds(0.1f);
            
            foreach (var card in _deckCards)
            {
                Debug.Log("s");
                card.transform.DORotate(new Vector3(0, 0, 0), 0.25f);

                yield return waitForOneSecond;
            }
        }
    }
}