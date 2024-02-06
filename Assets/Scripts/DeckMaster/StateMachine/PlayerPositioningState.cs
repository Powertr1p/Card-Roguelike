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
            SetPlayerPosition();

            var openStartpos = PositionPlacements[0].Data.Position + Vector2Int.up;
            var endStartPos = PositionPlacements[^1].Data.Position + Vector2Int.up;
            
            var cards =  GetCardsAroundPlayer(openStartpos, endStartPos, FaceSate.FaceUp);
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

        private void SetPlayerPosition()
        {
            var centerCard = PositionPlacements.Count / 2;
            Player.transform.position = new Vector3(PositionPlacements[centerCard].transform.position.x, PositionPlacements[centerCard].transform.position.y - Spawner.Offset.y, Player.transform.position.z);
            
            Player.Initialize(PositionPlacements[centerCard].Data);

            Player.GetComponent<DragAndDropObject>().SetNewInitialPosition(Player.transform.position);
        }

        private void ProcessState()
        {
            Process();
        }
    }
}