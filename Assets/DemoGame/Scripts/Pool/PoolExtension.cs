using System.Collections.Generic;
using UnityEngine;

namespace DemoGame.Scripts.Pool
{
    public static class PoolExtension
    {
        /// <summary>
        /// Objeyi kapatır ve poola gönderir.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="pool"></param>
        public static void DropPoolObject(this GameObject gameObject,Queue<GameObject> pool) 
        {
            gameObject.SetActive(false);
            pool.Enqueue(gameObject);
        }
        /// <summary>
        /// Objeyi açar ve pooldan döndürür.
        /// </summary>
        /// <param name="pool"></param>
        /// <returns></returns>
        public static GameObject GetPoolObject(this Queue<GameObject> pool)
        {
            var gameObject = pool.Dequeue();
            gameObject.SetActive(true);
            return gameObject;
        }
    }
}