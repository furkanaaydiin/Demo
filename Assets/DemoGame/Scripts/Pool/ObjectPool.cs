using System.Collections.Generic;
using UnityEngine;

namespace DemoGame.Scripts.Pool
{
   public class ObjectPool : MonoBehaviour
   {
      [SerializeField] private PoolObjectData[] poolObjectData;
      private static Dictionary<PoolObjectType,Queue<GameObject>> _poolList = new();
      private void Awake()
      {
         _poolList.Clear();
         for (var i = 0; i < poolObjectData.Length; i++) //Inspectorde verdiğimiz pool object dataları dönüyoruz.
         {
            var poolData = poolObjectData[i];
            var queue = new Queue<GameObject>(); //Yeni bir queue class'ı oluşturuluyor.
            for (var j = 0; j < poolData.pooledCount; j++) //Pool object dataya göre pool'a atılacak nesnelerin sayısı kadar dönüyor.
            {
               Instantiate(poolData.poolObject).DropPoolObject(queue); //Poola atılacak nesne instantiate ediliyor ve pool classına gönderiliyor
            }
            _poolList.Add(poolData.poolObjectType,queue); //Oluşturulan Queue<T> nesnesi PoolObjectType ile dictionary içerisine ekleniyor.
         }
      }
      /// <summary>
      /// Pooldan obje çekmeye yarar
      /// </summary>
      /// <param name="poolObjectType"></param>
      /// <returns></returns>
      public static GameObject GetPoolObject(PoolObjectType poolObjectType)
      {
        return _poolList[poolObjectType].GetPoolObject();
      }
      /// <summary>
      /// Poola obje göndermeye yarar
      /// </summary>
      /// <param name="poolObject">Poola gönderilecek GameObject</param>
      /// <param name="poolObjectType"></param>
      public static void DropPoolObject(GameObject poolObject,PoolObjectType poolObjectType)
      {
         poolObject.DropPoolObject(_poolList[poolObjectType]);
      }
   }
}