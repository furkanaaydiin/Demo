using DG.Tweening;
using UnityEngine;

namespace DemoGame.Scripts.Components
{
    public class ScaleObjectTween : MonoBehaviour
    {
        private void Awake()
        {
            EatenObjectTween();
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        private void EatenObjectTween()
        {
           transform.DOScale(Vector3.one * 0.5f, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
    }
}