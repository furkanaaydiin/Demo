using DemoGame.Scripts.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DemoGame.Scripts.Manager
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private float spawnTime;
        [SerializeField] private float spawnDelay;
        
        [SerializeField] private Transform centerPoint;
        [SerializeField] private float circleRadius;

        private void Start()
        {
            InvokeRepeating(nameof(SpawnObjectScalePickup), spawnTime, spawnDelay);
            InvokeRepeating(nameof(SpawnObjectSpeedPickup), 5f, 18f);
        }

        /// <summary>
        ///  burda referans olan scalemizi arttıracak olan nesnemizi yaratıyoruz
        /// </summary>
        public void SpawnObjectScalePickup()
        {
            var spawnPos = GetRandomPointInCircle(centerPoint.position, circleRadius);
            var obj = ObjectPool.GetPoolObject(PoolObjectType.ScalePickup);
            obj.transform.position = spawnPos;
        }

        /// <summary>
        /// Burda referans olan hızımızı arttıracak olan nesnemizi yaratıyoruz
        /// </summary>
        public void SpawnObjectSpeedPickup()
        {
            var spawnPos = GetRandomPointInCircle(centerPoint.position, circleRadius);
            var obj = ObjectPool.GetPoolObject(PoolObjectType.SpeedPickup);
            obj.transform.position = spawnPos;
        }

        /// <summary>
        /// Daire içerisinden random bir point döndürür
        /// </summary>
        private Vector3 GetRandomPointInCircle(Vector3 center, float radius)
        {
            var random = Random.insideUnitCircle * radius;
            return center + new Vector3(random.x, .5f, random.y);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private void OnDrawGizmos()
        {
            if (centerPoint == null) return;
            var color = Color.green;
            color.a = .3f;
            Gizmos.color = color;
            Gizmos.DrawSphere(centerPoint.position, circleRadius);
        }
    }
}