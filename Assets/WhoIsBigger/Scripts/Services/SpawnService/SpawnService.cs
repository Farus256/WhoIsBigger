using UnityEngine;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Views.Capsule;

namespace WhoIsBigger.Scripts.Services.SpawnService
{
    public class SpawnService : ISpawnService
    {
        private readonly CapsuleFactory _capsuleFactory;

        public SpawnService(CapsuleFactory capsuleFactory)
        {
            _capsuleFactory = capsuleFactory;
        }

        public CapsuleController SpawnCapsule(CapsuleType type, Vector3 position)
        {
            return _capsuleFactory.Create(type, position);
        }

        public void DestroyCapsule(CapsuleController capsule)
        {
            if (capsule != null)
                Object.Destroy(capsule.gameObject);
        }
    }
}