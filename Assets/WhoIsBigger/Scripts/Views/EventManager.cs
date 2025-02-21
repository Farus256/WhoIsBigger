using UnityEngine;
using UnityEngine.Events;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Views.Capsule;

namespace WhoIsBigger.Scripts.Views
{
    public class UnitSpawnEvent: UnityEvent<CapsuleType, Vector3>{}
    public class UnitDieEvent: UnityEvent<CapsuleController>{}
    
    public class EventManager
    {
        // Ивент спавна юнита
        public readonly UnitSpawnEvent OnUnitSpawn = new UnitSpawnEvent();
        
        // Ивент смерти юнита
        public readonly UnitDieEvent OnUnitDie = new UnitDieEvent();
    }
}