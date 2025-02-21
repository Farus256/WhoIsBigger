using UnityEngine;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Views;
using WhoIsBigger.Scripts.Views.Capsule;

namespace WhoIsBigger.Scripts.Services.CapsuleService
{
    public interface IRegisterService
    {
        void RegisterCapsule(CapsuleController capsule);
        void UnregisterCapsule(CapsuleController capsule);
        void RegisterSphere(SphereController sphere);
        void UnregisterSphere(SphereController sphere);
        
        CapsuleController FindNearestTarget(EntityType entityType, Vector3 position);
        SphereController FindNearestSphere(EntityType entityType, Vector3 position);
    }
}