using UnityEngine;

namespace DemoGame.Scripts.TargetSystem
{
    public class TargetBase : MonoBehaviour
    {
        protected virtual void Awake() => TargetManager.AddTarget(this);

        protected virtual void OnDestroy() => TargetManager.RemoveTarget(this);
    }
}