using System;
using DefaultNamespace.Effects.Enums;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] private int _healthPoints;
    [SerializeField] private int _shieldPoints;

    [SerializeField] private int _maxHealthValue = 12;

    public event Action<int> HealthValueChanged;
    public event Action<int> ShieldValueChanged;
    
    private void Start()
    {
        _healthPoints = _maxHealthValue;
        _shieldPoints = 0;
        
        HealthValueChanged?.Invoke(_healthPoints);
        ShieldValueChanged?.Invoke(_shieldPoints);
    }

    public void DecreaseHealth(int amount)
    {
        int shieldDamage = Mathf.Min(amount, _shieldPoints);
        DecreaseShield(shieldDamage);
    
        _healthPoints = Mathf.Max(_healthPoints - (amount - shieldDamage), 0);
        HealthValueChanged?.Invoke(_healthPoints);
    }

    public void IncreaseHealth(int amount)
    {
        _healthPoints = Math.Min(_healthPoints + amount, _maxHealthValue);
        HealthValueChanged?.Invoke(_healthPoints);
    }

    public void IncreaseShield(int amount)
    {
        _shieldPoints += amount;
        ShieldValueChanged?.Invoke(_shieldPoints);
    }
    
    public void DecreaseShield(int amount)
    {
        _shieldPoints = Math.Max(_shieldPoints - amount, 0);
        ShieldValueChanged?.Invoke(_shieldPoints);
    }
}
