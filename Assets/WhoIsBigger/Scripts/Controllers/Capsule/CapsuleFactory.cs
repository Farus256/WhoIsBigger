﻿using UnityEngine;
using WhoIsBigger.Scripts.View;
using Zenject;

namespace WhoIsBigger.Scripts.Presenter
{
    public class CapsuleFactory : PlaceholderFactory<CapsuleType, Vector3, CapsuleController>
    {
        private readonly DiContainer _container;
        private readonly GameObject _friendlyCapsulePrefab;
        private readonly GameObject _enemyCapsulePrefab;
        
        [Inject] private EventManager _eventManager;
        public CapsuleFactory(
            DiContainer container, 
            [Inject(Id = "FriendlyCapsulePrefab")] GameObject friendlyCapsulePrefab, 
            [Inject(Id = "EnemyCapsulePrefab")] GameObject enemyCapsulePrefab)
        {
            _container = container;
            _friendlyCapsulePrefab = friendlyCapsulePrefab;
            _enemyCapsulePrefab = enemyCapsulePrefab;
        }
            
        public override CapsuleController Create(CapsuleType capsuleType, Vector3 pos)
        {
            GameObject prefabToInstance = null;
            switch (capsuleType)
            {
                case CapsuleType.Enemy:
                    prefabToInstance = _enemyCapsulePrefab;
                    break;
                case CapsuleType.Friendly:
                    prefabToInstance = _friendlyCapsulePrefab;
                    break;
                default:
                    Debug.LogError($"Unknown capsule type: {capsuleType}");
                    return null;
            }
            
            GameObject instance = _container.InstantiatePrefab(prefabToInstance, new GameObjectCreationParameters { Position = pos });

            CapsuleController controller = instance.GetComponent<CapsuleController>();
            if(controller == null)
            {
                Debug.LogError($"No CapsuleController found on {instance.name}");
                return null;
            }
            
            _container.Inject(controller);
            controller.Construct(capsuleType,pos);
            
            _eventManager.OnUnitSpawned.Invoke(capsuleType);
            
            return instance.GetComponent<CapsuleController>();
        }
    }
}