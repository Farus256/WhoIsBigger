using UnityEngine;
using UnityEngine.Events;

namespace WhoIsBigger.Scripts
{
    public class UnitSpawnedEvent: UnityEvent<CapsuleType> { }
    
    public class UnitDiedEvent: UnityEvent<CapsuleType> { }
    
    public class FriendlyDiedEvent: UnityEvent { }
    public class EnemyDiedEvent: UnityEvent { }
    
    public class EventManager : MonoBehaviour
    {
        // Ивент спавна юнита
        public UnitSpawnedEvent OnUnitSpawned = new UnitSpawnedEvent();
        
        // Ивент смерти юнита
        public UnitDiedEvent OnUnitDied = new UnitDiedEvent();
    }
}