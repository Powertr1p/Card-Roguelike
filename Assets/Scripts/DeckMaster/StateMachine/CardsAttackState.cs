using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards;
using DefaultNamespace.Player;
using UnityEngine;

namespace DeckMaster.StateMachine
{
    public class CardsAttackState : State
    {
        public CardsAttackState(PlayerInput input, List<DeckCard> deckCards, PlayerHeroCard playerCard, MonoBehaviour mono) : base(input, deckCards, playerCard, mono)
        {
            Name = TurnState.CardsAttackState;
        }
        
        public override void Enter()
        {
            Input.DisableInput();
            base.Enter();
        }

        public override void Execute()
        {
            var attackZone = GameRulesGetter.EnemyAttackZone;
            
            var cardsWithPossibleAttack = GetCardsAroundPlayer(Player.PositionData.Position - attackZone, Player.PositionData.Position + attackZone, FaceSate.FaceDown);
            Mono.StartCoroutine(GetCardsThatCanAttackPlayer(cardsWithPossibleAttack, MoveToNextState));
        }

        public override void Exit()
        {
            base.Exit();
        }

        private IEnumerator GetCardsThatCanAttackPlayer(List<DeckCard> cardsAround, Action completeCallback)
        {
            foreach (var card in cardsAround)
            {
                if (!card.TryGetComponent(out EnemyCard enemy)) continue;
                
                var attackPositions = enemy.GetTargetAttackPositions();
                if (IsPlayerInAttackPosition(attackPositions))
                {
                    yield return Mono.StartCoroutine(enemy.PerformAttack(Player.transform.position, Player));
                }
            }
            
            completeCallback?.Invoke();
        }

        private void MoveToNextState()
        { 
            NextState = new OpenCardsState(Input, DeckCards, Player, Mono);
            base.Execute();
            Process();
        }

        private bool IsPlayerInAttackPosition(List<Vector2Int> attackPositions)
        {
            return attackPositions.Any(attackPosition => Player.PositionData.Position == attackPosition);
        }
    }
}