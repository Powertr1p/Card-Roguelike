using System;
using DefaultNamespace.Effects.Enums;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _healthPoints;
    [SerializeField] private int _shieldPoints;

    [SerializeField] private int _maxHealthValue = 12;
    [SerializeField] private int _maxShieldValue = 12;

    private void Awake()
    {
        _healthPoints = _maxHealthValue;
        _shieldPoints = _maxShieldValue;
    }

    public void DecreaseHealth(int amount, AffectType type)
    {
        _healthPoints = Math.Max(_healthPoints - amount, 0);
    }

    public void IncreaseHealth(int amount, AffectType type)
    {
        _healthPoints += amount;
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
