using Cards;
using UnityEngine;

namespace DeckMaster.Factory
{
    public class EnemyFactory : EffectCardsFactory<EnemyCard>
    {
        public override EnemyCard CreateNewInstance(int col, int row, int position, Vector2 offset)
        {
            var instance = base.CreateNewInstance(col, row, position, offset);
            instance.SetEffects(SetRandomizeEffects());

            return instance;
        }
        
    }
}