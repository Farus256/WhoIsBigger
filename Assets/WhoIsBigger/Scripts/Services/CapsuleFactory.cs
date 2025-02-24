﻿using UnityEngine;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Views;
using WhoIsBigger.Scripts.Views.Capsule;
using Zenject;

namespace WhoIsBigger.Scripts.Services
{
    public class CapsuleFactory : PlaceholderFactory<EntityType, Vector3, CapsuleController>
    {
        private readonly DiContainer _container;
        private readonly GameObject _friendlyCapsulePrefab;
        private readonly GameObject _enemyCapsulePrefab;
        private readonly Transform _capsulesContainer;
        
        [Inject] private EventManager _eventManager;
        public CapsuleFactory(
            DiContainer container, 
            [Inject(Id = "FriendlyCapsulePrefab")] GameObject friendlyCapsulePrefab, 
            [Inject(Id = "EnemyCapsulePrefab")] GameObject enemyCapsulePrefab,
            [Inject(Id = "CapsulesContainer")] Transform capsulesContainer)
        {
            _container = container;
            _friendlyCapsulePrefab = friendlyCapsulePrefab;
            _enemyCapsulePrefab = enemyCapsulePrefab;
            _capsulesContainer = capsulesContainer;
        }
            
        public override CapsuleController Create(EntityType entityType, Vector3 pos)
        {
            GameObject prefabToInstance = null;
            switch (entityType)
            {
                case EntityType.Enemy:
                    prefabToInstance = _enemyCapsulePrefab;
                    break;
                case EntityType.Friendly:
                    prefabToInstance = _friendlyCapsulePrefab;
                    break;
                default:
                    Debug.LogError($"Unknown capsule type: {entityType}");
                    return null;
            }
            
            GameObject instance = _container.InstantiatePrefab(prefabToInstance, new GameObjectCreationParameters
            {
                ParentTransform = _capsulesContainer,
                Position = pos
            });

            CapsuleController controller = instance.GetComponent<CapsuleController>();
            if(controller == null)
            {
                Debug.LogError($"No CapsuleController found on {instance.name}");
                return null;
            }
            
            _container.Inject(controller);
            controller.Construct(entityType,pos);
            
            return instance.GetComponent<CapsuleController>();
        }
    }
}