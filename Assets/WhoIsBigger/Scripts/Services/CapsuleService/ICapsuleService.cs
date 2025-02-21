using UnityEngine;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Views.Capsule;

namespace WhoIsBigger.Scripts.Services.CapsuleService
{
    public interface ICapsuleService
    {
        void RegisterCapsule(CapsuleController capsule);
        void UnregisterCapsule(CapsuleController capsule);
        CapsuleController FindNearestTarget(CapsuleType capsuleType, Vector3 position);
    }
}