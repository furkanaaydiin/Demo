using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace DemoGame.Scripts.Components
{
    public class GameOverTimer : MonoBehaviour
    {
        public static UnityAction OnTimeOver;
        private float _timeLeft = 60;    
        [SerializeField] private TextMeshProUGUI text;
        private bool _isOver;
        private void Awake()
        {
            OnTimeOver += OnGameOver;
        }
        private void OnDestroy()
        {
            OnTimeOver -= OnGameOver;
        }
        private void Update()
        {
            if (_isOver)
                return;
            Timer();
        }

        private void OnGameOver()
        {
            _isOver = true;
        }
        private void Timer()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
            {
                OnTimeOver?.Invoke();
                _timeLeft = 0;
            }
            text.text =$"{(int)_timeLeft}";
        }
    }
}
