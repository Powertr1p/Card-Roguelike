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
            base.Enter();
        }

        public override void Execute()
        {
            var cardsWithPossibleAttack = GetCardsAroundPlayer(Player.Data.Position - Vector2Int.one, Player.Data.Position + Vector2Int.one, FaceSate.FaceDown);
            GetCardsThatCanAttackPlayer(cardsWithPossibleAttack);
            
            NextState = new OpenCardsState(Input, DeckCards, Player, Mono);
            
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void GetCardsThatCanAttackPlayer(List<DeckCard> cardsAround)
        {
            foreach (var card in cardsAround)
            {
                if (!card.TryGetComponent(out EnemyCard enemy)) continue;
                
                var attackPositions =  enemy.GetTargetAttackPositions();

                if (IsPlayerInAttackPosition(attackPositions))
                {
                    Debug.LogError("PLAYER GOT DAMAGED!");
                }
            }
        }

        private bool IsPlayerInAttackPosition(List<Vector2Int> attackPositions)
        {
            return attackPositions.Any(attackPosition => Player.Data.Position == attackPosition);
        }
    }
}