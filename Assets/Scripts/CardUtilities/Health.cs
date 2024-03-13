using System;
using Data;
using DeckMaster;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _healthPoints;
    private int _shieldPoints;
    private int _maxHealth;

    public event Action<int> HealthValueChanged;
    public event Action<int> ShieldValueChanged;
    
    private void Start()
    {
        _maxHealth = GameRulesGetter.Rules.PlayerMaxHealth;

        _healthPoints = PlayerStatsStorage.Health;
        _shieldPoints = PlayerStatsStorage.Shield;

        HealthValueChanged?.Invoke(_healthPoints);
        ShieldValueChanged?.Invoke(_shieldPoints);
    }

    public void DecreaseHealth(int amount, bool ignoreShield)
    {
        if (ignoreShield)
        {
            _healthPoints = Math.Max(_healthPoints - amount, 0);
            PlayerStatsStorage.Health = _healthPoints;
            
            HealthValueChanged?.Invoke(_healthPoints);
            
            return;
        }
        
        int shieldDamage = Mathf.Min(amount, _shieldPoints);
        DecreaseShield(shieldDamage);
    
        _healthPoints = Mathf.Max(_healthPoints - (amount - shieldDamage), 0);
        PlayerStatsStorage.Health = _healthPoints;
        
        HealthValueChanged?.Invoke(_healthPoints);
    }

    public void IncreaseHealth(int amount)
    {
        if (GameRulesGetter.Rules.OverhealWithDamage)
            _healthPoints += amount;
        else
            _healthPoints = Math.Min(_healthPoints + amount, _maxHealth);

        HealthValueChanged?.Invoke(_healthPoints);
    }

    public void IncreaseShield(int amount)
    {
        _shieldPoints += amount;
        PlayerStatsStorage.Shield = _shieldPoints;
        
        ShieldValueChanged?.Invoke(_shieldPoints);
    }
    
    public void DecreaseShield(int amount)
    {
        _shieldPoints = Math.Max(_shieldPoints - amount, 0);
        PlayerStatsStorage.Shield = _shieldPoints;
        
        ShieldValueChanged?.Invoke(_shieldPoints);
    }

    public void TryDamageOverheal()
    {
        if (!GameRulesGetter.Rules.OverhealWithDamage) return;
        
        if (_healthPoints > _maxHealth)
        {
            DecreaseHealth(1, true);
        }
    }
}
