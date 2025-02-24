﻿using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Models;
using WhoIsBigger.Scripts.Services;
using Zenject;

namespace WhoIsBigger.Scripts.Views
{
    public class UnitSpawner : MonoBehaviour
    {
        [Inject] private EventManager _eventManager;
        [Inject] private IGameModel _gameModel;
        
        public float interval = 1;
        public float3 size = new float3(1, 0, 1);
        public int count;
        [FormerlySerializedAs("capsuleType")] public EntityType entityType;
        
        private float _elapsed;
        private Unity.Mathematics.Random _random = new Unity.Mathematics.Random(1);

        private void Update()
        {
            if (_gameModel.MaxUnitsCount == count)
                return;

            _elapsed += Time.deltaTime;
            if (_elapsed >= interval)
            {
                float3 offset = _random.NextFloat3(-size, size);
                float3 position = (float3) transform.position + offset;
                
                _eventManager.OnUnitSpawn.Invoke(entityType, position);
                
                _elapsed -= interval;
                count++;
            }
        }
    }
}