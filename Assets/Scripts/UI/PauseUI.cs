using UnityEngine;

namespace UI
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] private UIScreen _pauseUI;
        private bool _paused = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SwitchPauseUI();
            }
        }

        private void SwitchPauseUI()
        {
            if (!_paused)
            {
                _pauseUI.Show();
                _paused = false;
            }
            else
            {
                _pauseUI.Hide();
                _paused = true;
            }
        }
    }
}