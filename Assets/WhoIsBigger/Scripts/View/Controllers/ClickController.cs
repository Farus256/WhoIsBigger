using UnityEngine;
using Zenject;
using System;

namespace WhoIsBigger.Scripts.View
{
    public class ClickController : MonoBehaviour
    {
        [Inject] private EventManager _gameEvents;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.Log("hi");
                    _gameEvents.OnUnitSpawned.Invoke();
                }
            }
        }
    }
}