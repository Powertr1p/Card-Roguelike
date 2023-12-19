using System.Collections.Generic;
using Cards;
using CardUtilities;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DeckMaster.Factory
{
    public class EnemyFactory : EffectCardsFactory<EnemyCard>
    {
        [BoxGroup("Attack Params")]
        [SerializeField] private DirectionAttackPositionaData _attackData;
        [BoxGroup("Attack Params")]
        [SerializeField] private int _maxDirections = 3;
        [BoxGroup("Attack Params")]
        [SerializeField] private int _minDirections = 1;
        
        private List<DirectionAttackPosition> _attackDirections;

        private void Awake()
        {
            ValidateAttackParams();
        }

        public override EnemyCard CreateNewInstance(int col, int row, int position, Vector2 offset)
        {
            var instance = base.CreateNewInstance(col, row, position, offset);
            instance.SetEffects(SetRandomizeEffects());
            instance.SetAttackDirections(GetRandomAttackDirections());

            return instance;
        }

        private void ValidateAttackParams()
        {
            if (_minDirections > _attackData.GetPossibleDirectionsCount())
            {
                _minDirections = _attackData.GetPossibleDirectionsCount();
                Debug.LogError("Minimum attack direction is more than data have!");
            }

            if (_maxDirections > _attackData.GetPossibleDirectionsCount())
            {
                _maxDirections = _attackData.GetPossibleDirectionsCount();
                Debug.LogError("Maximum attack direction is more than data have!");
            }
        }

        private List<DirectionAttackPosition> GetRandomAttackDirections()
        {
            _attackDirections = new List<DirectionAttackPosition>();
            
            var attackDirectionsCount = Random.Range(1, _attackData.GetPossibleDirectionsCount());

            for (int i = 0; i < attackDirectionsCount; i++)
            {
                var randomAttackDirection = (AttackDirection) Random.Range(0, _attackData.GetPossibleDirectionsCount());
                DirectionAttackPosition attack = new DirectionAttackPosition(randomAttackDirection, _attackData.GetAttackPosition(randomAttackDirection));

                if (_attackDirections.Contains(attack)) continue;
                
                _attackDirections.Add(attack);
            }

            return _attackDirections;
        }
    }
}