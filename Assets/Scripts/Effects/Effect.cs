using DefaultNamespace.Effects.Enums;
using NaughtyAttributes;
using UnityEngine;

namespace DefaultNamespace.Effects
{
    [CreateAssetMenu(fileName = "Effect", menuName = "Effect", order = 0)]
    public class Effect : ScriptableObject
    {
        [SerializeField] private EffectType _effectType;
        [SerializeField] private AffectParameter _affectParameter;
        [SerializeField] private AffectType _affectType;
        [SerializeField] private ParticleSystem _effectParticle;

        [ShowIf("_affectType", AffectType.Turns)]
        [SerializeField] private int _duration;

        [SerializeField] private int _amount;

        [Header("Visual Data")]
        [SerializeField] private CardVisualData _visual;

        public CardVisualData VisualData => _visual;
        public ParticleSystem EffectParticle => _effectParticle;
        public EffectType EffectType => _effectType;
        public AffectParameter AffectParameter => _affectParameter;
        public AffectType AffectType => _affectType;
        public int Duration => _duration;
        public int Amount => _amount;
    }
}