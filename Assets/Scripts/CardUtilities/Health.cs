using System;
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

    public void DecreaseHealth(int amount)
    {
        _healthPoints = Math.Max(_healthPoints - amount, 0);
    }

    public void IncreaseHealth(int amount)
    {
        _shieldPoints = Math.Max(_shieldPoints - amount, 0);
    }
}
