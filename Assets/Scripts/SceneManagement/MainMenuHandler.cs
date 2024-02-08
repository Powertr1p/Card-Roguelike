using UnityEngine;
using UnityEngine.UI;

namespace SceneManagement
{
    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField] private Button _gameStartButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            _gameStartButton.onClick.AddListener(StartGame);
            _exitButton.onClick.AddListener(TerminateApplication);
        }

        private void OnDisable()
        {
            _gameStartButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        private void StartGame()
        {
            SceneLoader.LoadScene("Game");
        }

        private void TerminateApplication()
        {
            Application.Quit();
        }
    }
}