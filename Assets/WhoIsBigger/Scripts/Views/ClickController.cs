using UnityEngine;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Services;
using Zenject;

namespace WhoIsBigger.Scripts.Views
{
    public class ClickController : MonoBehaviour
    {
        [Inject] private EventManager _eventManager;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SpawnUnit(CapsuleType.Friendly);
            }

            if (Input.GetMouseButtonUp(1))
            {
                SpawnUnit(CapsuleType.Enemy);
            }
        }

        private void SpawnUnit(CapsuleType capsuleType)
        {
            Vector3 clickPos = HandleClick();
                
            if (clickPos == Vector3.zero)
                return;
            
            _eventManager.OnUnitSpawn.Invoke(capsuleType, clickPos + Vector3.up);
        }
        
        private Vector3 HandleClick()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit.point;
            }

            return Vector3.zero;
        }
    }
}