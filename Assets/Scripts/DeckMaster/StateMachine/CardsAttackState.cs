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
            var cardsWithPossibleAttack = GetCardsAroundPlayer(Player.Data.Position - Vector2Int.one, Player.Data.Position + Vector2Int.one, FaceSate.FaceDown);
            Mono.StartCoroutine(GetCardsThatCanAttackPlayer(cardsWithPossibleAttack));
        }

        public override void Exit()
        {
            base.Exit();
        }

        private IEnumerator GetCardsThatCanAttackPlayer(List<DeckCard> cardsAround)
        {
            foreach (var card in cardsAround)
            {
                if (!card.TryGetComponent(out EnemyCard enemy)) yield return null;
                
                var attackPositions = enemy.GetTargetAttackPositions();

                if (IsPlayerInAttackPosition(attackPositions))
                {
                    yield return Mono.StartCoroutine(enemy.PerformAttack(Player.transform.position));
                }
            }
            
            NextState = new OpenCardsState(Input, DeckCards, Player, Mono);
            base.Execute();
            Process();
        }

        private bool IsPlayerInAttackPosition(List<Vector2Int> attackPositions)
        {
            return attackPositions.Any(attackPosition => Player.Data.Position == attackPosition);
        }
    }
}