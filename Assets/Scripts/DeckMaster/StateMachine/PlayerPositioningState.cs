using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using DefaultNamespace.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DeckMaster.StateMachine
{
    public class PlayerPositioningState : State
    {
        public PlayerPositioningState(PlayerInput input, List<DeckCard> deckCards, PlayerHeroCard playerCard, MonoBehaviour mono, DeckSpawner spawner) : base(input, deckCards, playerCard, mono, spawner)
        {
            Name = TurnState.PlayerPositioningTurn;
        }

        public override void Enter()
        {
            Input.DisableInput();
            Player.EventTurnEnded += ProcessState;
            base.Enter();
        }

        public override void Execute()
        {
            PositionPlacements = Spawner.SpawnPlacementsForPlayer();
            var cards =  GetCardsAroundPlayer(new Vector2Int(0,0), new Vector2Int(Spawner.Rows - 1,0), FaceSate.FaceUp);
            Mono.StartCoroutine(OpenCards(cards, MoveToNextState));
        }

        public override void Exit()
        {
            Player.EventTurnEnded -= ProcessState;
            
            foreach (var placement in PositionPlacements)
            {
                Object.Destroy(placement.gameObject);
            }
            
            base.Exit();
            
        }
        
        private IEnumerator OpenCards(List<DeckCard> cardsToOpen, Action completeCallback)
        {
            foreach (var card in cardsToOpen)
            {
                card.OpenCard();

                yield return new WaitForSeconds(0.1f);
            }
            
            completeCallback?.Invoke();
        }

        private void MoveToNextState()
        {
            NextState = new CardsAttackState(Input, DeckCards, Player, Mono);
            base.Execute();
            Input.EnableInput();
        }

        private void ProcessState()
        {
            Process();
        }
    }
}