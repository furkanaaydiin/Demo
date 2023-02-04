using System.Linq;
using DemoGame.Scripts.Agent;
using DemoGame.Scripts.TargetSystem;

namespace DemoGame.Scripts.Pool.PoolObjects
{ public class Pickup : TargetBase
    { public void OnTriggerDelete()
        {
            var pickupGameObject = gameObject;
            var enemyControllers = FindObjectsOfType<Enemy>().ToList();
            foreach (var enemyController in enemyControllers)
                enemyController.RemoveArray(pickupGameObject);
 
            pickupGameObject.tag = "PickedUp";
            pickupGameObject.SetActive(false);
            pickupGameObject.tag = "Target";
        }
    }
    
    
}