using System.Collections.Generic;
using Cards;
using CardUtilities;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DeckMaster.Factory
{
    public class EnemyFactory : EffectCardsFactory<EnemyCard>
    {
        [SerializeField] private DirectionAttackPositionaData _attackData;
        [SerializeField] private int _maxDirections = 3;
        [SerializeField] private int _minDirections = 1;
        
        private List<DirectionAttackPosition> _attackDirections;

        private void Awake()
        {
            ValidateAttackParams();
        }

        public override EnemyCard CreateNewInstance(int col, int row, Vector2 worldPosition, Transform parent, CardData data)
        {
            var instance = base.CreateNewInstance(col, row, worldPosition, parent, data);
            
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