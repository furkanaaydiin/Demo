using DG.Tweening;
using UnityEngine;

namespace DemoGame.Scripts.Components
{
    public class SpeedObjectShakeTween : MonoBehaviour
    {
        private void Awake()
        {
            SpeedShakeTween();
        }

        private void SpeedShakeTween()
        {
            gameObject.transform.DOShakePosition(10, 2f, 20, 10f);
        }
    }
}