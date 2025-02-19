using UnityEngine;
using UnityEngine.Events;

namespace WhoIsBigger.Scripts
{
    
    public class UnitSpawnedEvent: UnityEvent { }
    
    public class EventManager : MonoBehaviour
    {
        // Ивент спавна юнита
        public UnitSpawnedEvent OnUnitSpawned = new UnitSpawnedEvent();
    }
}