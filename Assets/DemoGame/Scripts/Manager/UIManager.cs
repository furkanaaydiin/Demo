using DemoGame.Scripts.Camera;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;
using CameraType = DemoGame.Scripts.Camera.CameraType;

namespace DemoGame.Scripts.Manager
{
    public class UIManager : MonoBehaviour
    {
        [Header("GamePanels")] [SerializeField]
        private GameObject startGamePanel;

        [SerializeField] public GameObject resumePanel;

        [Header("GameButton")] [SerializeField]
        private Button pauseButton;

        [SerializeField] private Button resumeButton;
        [SerializeField] private Button startButton;

        private void Awake()
        {
            Time.timeScale = 0;
            pauseButton.gameObject.SetActive(false);
            pauseButton.onClick.AddListener(StopTheGame);
            resumeButton.onClick.AddListener(ResumeButton);
            startButton.onClick.AddListener(StartGame);
       
        }
        private void StopTheGame() => Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        private void ResumeButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void StartGame()
        {
            startGamePanel.SetActive(false);
            pauseButton.gameObject.SetActive(true);
            CameraController.Instance.ChangeCamera(CameraType.FollowCamera);
            Time.timeScale = 1;
        }
        
    }
}