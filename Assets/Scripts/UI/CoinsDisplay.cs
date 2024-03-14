using Cards;
using Data;
using TMPro;
using UnityEngine;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private HeroCard _player;
    [SerializeField] private TextMeshProUGUI _coinsText;

    private void Start()
    {
        UpdateCoinsText(PlayerStatsStorage.Coins);
    }

    private void OnEnable()
    {
        _player.CoinsAdded += UpdateCoinsText;
    }

    private void OnDisable()
    {
        _player.CoinsAdded -= UpdateCoinsText;
    }

    private void UpdateCoinsText(int newValue)
    {
        _coinsText.text = newValue.ToString();
    }
}
