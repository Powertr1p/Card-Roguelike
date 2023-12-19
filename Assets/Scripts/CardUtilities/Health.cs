using System;
using DefaultNamespace.Effects.Enums;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] private int _healthPoints;
    [SerializeField] private int _shieldPoints;

    [SerializeField] private int _maxHealthValue = 12;
    [SerializeField] private int _maxShieldValue = 12;

    public event Action<int> HealthValueChanged;
    public event Action<int> ShieldValueChanged;
    
    private void Start()
    {
        _healthPoints = _maxHealthValue;
        _shieldPoints = 0;
        
        HealthValueChanged?.Invoke(_healthPoints);
        ShieldValueChanged?.Invoke(_shieldPoints);
    }

    public void DecreaseHealth(int amount, AffectType type)
    {
        _healthPoints = Math.Max(_healthPoints - amount, 0);
        HealthValueChanged?.Invoke(_healthPoints);
    }

    public void IncreaseHealth(int amount, AffectType type)
    {
        _healthPoints = Math.Min(_healthPoints + amount, _maxHealthValue);
        HealthValueChanged?.Invoke(_healthPoints);
    }

    public void IncreaseShield(int amount, AffectType type)
    {
        _shieldPoints = Math.Min(_shieldPoints + amount, _maxShieldValue);
        ShieldValueChanged?.Invoke(_shieldPoints);
    }
    
    public void DecreaseShield(int amount, AffectType type)
    {
        _shieldPoints = Math.Max(_shieldPoints - amount, 0);
        ShieldValueChanged?.Invoke(_shieldPoints);
    }
}
