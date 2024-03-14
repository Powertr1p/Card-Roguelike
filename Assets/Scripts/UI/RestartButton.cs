using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnRestartClicked); 
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnRestartClicked()
        {
            SceneLoader.RestartScene();
        }
    }
}

