using UnityEngine;
using Zenject;
using System;
using WhoIsBigger.Scripts.Presenter;

namespace WhoIsBigger.Scripts.View
{
    public class ClickController : MonoBehaviour
    {
        [Inject] private CapsuleFactory _capsuleFactory;
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

            Debug.Log("программа поставила" + capsuleType);
            _capsuleFactory.Create(capsuleType, clickPos + Vector3.up);
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