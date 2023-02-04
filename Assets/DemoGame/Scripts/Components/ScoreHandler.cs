using TMPro;
using UnityEngine;

namespace DemoGame.Scripts.Components
{
    public class ScoreHandler : MonoBehaviour
    {
        public static ScoreHandler Instance;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI panelScoreText;
        private int _score;
        private int Score
        {
            get => _score;
            set
            {
                _score = value;
                scoreText.text = _score.ToString();
                panelScoreText.text = _score.ToString();
            }
        }
        private void Awake() => Instance = this;
        private void Start() => Score = 0;
        public void Scored(int value) => Score += value;
    }
}