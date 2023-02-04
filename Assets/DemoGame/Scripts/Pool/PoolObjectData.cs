using System;
using UnityEngine;

namespace DemoGame.Scripts.Pool
{
    [Serializable] 
    public class PoolObjectData
    {
        public PoolObjectType poolObjectType;
        public GameObject poolObject;
        public int pooledCount;
    }
}