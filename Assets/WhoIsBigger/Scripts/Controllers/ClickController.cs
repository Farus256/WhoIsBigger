using UnityEngine;
using Zenject;
using System;
using WhoIsBigger.Scripts.Presenter;

namespace WhoIsBigger.Scripts.View
{
    public class ClickController : MonoBehaviour
    {
        [Inject] private EventManager _gameEvents;
        [Inject] private FriendlyCapsuleFactory _friendlyCapsuleFactory;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.Log("hi");
                    _friendlyCapsuleFactory.Create(hit.point);
                    _gameEvents.OnUnitSpawned.Invoke();
                }
            }
        }
    }
}