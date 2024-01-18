using CardUtilities;
using DeckMaster;
using DefaultNamespace.Effects;
using DefaultNamespace.Effects.Enums;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(TurnEffectsHandler))]
    public class HeroCard : Card
    {
        [SerializeField] private int _coins;
        [SerializeField] private ParticleSystem _attackParticle;

        private Health _health;
        private TurnEffectsHandler _turnEffectsHandler;
        private EffectMapper _effectMapper;

        protected virtual void Awake()
        {
            _health = GetComponent<Health>();
            _turnEffectsHandler = GetComponent<TurnEffectsHandler>();
            
            _effectMapper = new EffectMapper(this);
        }

        private void OnEnable()
        {
            _turnEffectsHandler.ExecuteTurnEffect += ApplyTurnBasedEffect;
        }

        private void OnDisable()
        {
            _turnEffectsHandler.ExecuteTurnEffect -= ApplyTurnBasedEffect;
        }

        public override void Interact(HeroCard interactorCard)
        {
        }

        public void ApplyEffect(Effect effect)
        {
            if (IsEffectTurnAffect(effect))
                _turnEffectsHandler.AddTurnEffect(effect);
            else
                _effectMapper.GetEffect(effect).Invoke(effect.Amount, effect);
        }
        
        public void Heal(int amount, Effect effect)
        {
            _health.IncreaseHealth(amount);
        }

        public void GetHpDamage(int amount, Effect effect)
        {
            _health.DecreaseHealth(amount, effect.IgnoreShield);
        }

        public void AddShield(int amount, Effect effect)
        {
            _health.IncreaseShield(amount);
        }

        public void GetShieldDamage(int amount, Effect effect)
        {
            _health.DecreaseShield(amount);
        }

        public void AddCoins(int amount, Effect effect)
        {
            _coins += amount;
        }

        public void TryExecuteTurnEffects()
        {
            _turnEffectsHandler.TryExecuteEffects();
            _health.TryDamageOverheal();
        }
        
        protected void PlayParticleAttack()
        {
            var particalInstance = Instantiate(_attackParticle, transform.position, Quaternion.identity);
            particalInstance.Play();
        }

        private bool IsEffectTurnAffect(Effect effect)
        {
            return effect.AffectType == AffectType.Turns;
        }

        private void ApplyTurnBasedEffect(Effect effect)
        {
            _effectMapper.GetEffect(effect).Invoke(effect.Amount, effect);
            PlayTurnEffectParticles(effect);
        }

        private void PlayTurnEffectParticles(Effect effect)
        {
            var particle = Instantiate(effect.EffectParticle, transform.position + GameRulesGetter.Rules.VFXOffset, Quaternion.identity);
            particle.Play();
        }
    }
}