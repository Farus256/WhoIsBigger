using ProjectDawn.Navigation.Hybrid;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

namespace WhoIsBigger.Scripts.Presenter
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private CapsuleFactory _capsuleFactory;
        
        public float interval = 1;
        public float3 size = new float3(1, 0, 1);
        public int count;
        public int maxCount = 1000;
        public CapsuleType capsuleType;
        
        private float _elapsed;
        private Unity.Mathematics.Random _random = new Unity.Mathematics.Random(1);

        void Update()
        {
            if (maxCount == count)
                return;

            _elapsed += Time.deltaTime;
            if (_elapsed >= interval)
            {
                float3 offset = _random.NextFloat3(-size, size);
                float3 position = (float3) transform.position + offset;
                
                _capsuleFactory.Create(capsuleType, position);
                
                _elapsed -= interval;
                count++;
            }
        }
    }
}