using DemoGame.Scripts.Agent;
using DemoGame.Scripts.Components;
using UnityEngine;

namespace DemoGame.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Enemy[] enemies;
        private int _enemyDeadCounter;
        private void Awake()
        {
            _enemyDeadCounter = 0;
            
            foreach (var enemy in enemies)
            {
                enemy.OnDestroyed += CheckEnemies;
            }
            GameOverTimer.OnTimeOver += GameOver;
        }

        private void CheckEnemies()
        {
            _enemyDeadCounter++;
            if (_enemyDeadCounter >= enemies.Length)
            {
                GameOverTimer.OnTimeOver?.Invoke();
            }
        }

        private void OnDestroy()
        {
            GameOverTimer.OnTimeOver -= GameOver;
            foreach (var enemy in enemies)
            {
                enemy.OnDestroyed -= CheckEnemies;
            }
        }

        private void GameOver()
        {
            uiManager.resumePanel.SetActive(true);
            uiManager.scorePanel.SetActive(false);
            Time.timeScale = 0;
        }
        
        
    }
}