using UnityEngine;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Services;
using WhoIsBigger.Scripts.Views.Capsule;
using Zenject;

namespace WhoIsBigger.Scripts.Views
{
    public class SpawnManager : MonoBehaviour
    {
        [Inject] private CapsuleFactory _capsuleFactory;
        [Inject] private EventManager _eventManager;
        
        private void OnEnable()
        {
            _eventManager.OnUnitSpawn.AddListener(HandleUnitSpawn);
            _eventManager.OnUnitDie.AddListener(HandleUnitDie);
        }
        
        private void OnDisable()
        {
            _eventManager.OnUnitSpawn.RemoveListener(HandleUnitSpawn);
            _eventManager.OnUnitDie.RemoveListener(HandleUnitDie);
        }
        
        private void HandleUnitSpawn(CapsuleType capsuleType, Vector3 pos)
        {
            // Вызываем фабрику для спавна капсулы
            _capsuleFactory.Create(capsuleType, pos);
        }

        private void HandleUnitDie(CapsuleController capsuleController)
        {
            Destroy(capsuleController.gameObject);
        }
    }
}