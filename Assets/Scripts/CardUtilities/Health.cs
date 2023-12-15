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
    
    private void Start()
    {
        _healthPoints = _maxHealthValue;
        _shieldPoints = _maxShieldValue;
        
        HealthValueChanged?.Invoke(_healthPoints);
    }

    public void DecreaseHealth(int amount, AffectType type)
    {
        _healthPoints = Math.Max(_healthPoints - amount, 0);
        HealthValueChanged?.Invoke(_healthPoints);
    }

    public void IncreaseHealth(int amount, AffectType type)
    {
        _healthPoints += amount;
        HealthValueChanged?.Invoke(_healthPoints);
    }

    public void IncreaseShield(int amount, AffectType type)
    {
        _shieldPoints += amount;
    }
    
    public void DecreaseShield(int amount, AffectType type)
    {
        _shieldPoints = Math.Max(_shieldPoints - amount, 0);
    }
}
