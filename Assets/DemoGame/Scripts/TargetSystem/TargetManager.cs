using System.Collections.Generic;
using System.Linq;
using DemoGame.Scripts.Agent;

namespace DemoGame.Scripts.TargetSystem
{
    public static class TargetManager
    {
         private static readonly List<TargetBase> ActiveTargets = new();
         public static void AddTarget(TargetBase targetBase) => ActiveTargets.Add(targetBase);
         public static void RemoveTarget(TargetBase targetBase) => ActiveTargets.Remove(targetBase);
         public static List<TargetBase> GetAllActiveTargets() =>ActiveTargets.Where(x=>x != null && x.gameObject.activeInHierarchy).ToList();
         public static List<Enemy> GetAlLEnemies() => ActiveTargets.Where(x=> x is Enemy).Cast<Enemy>().ToList();
    }
}