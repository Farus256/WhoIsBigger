using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Views;
using WhoIsBigger.Scripts.Views.Capsule;

namespace WhoIsBigger.Scripts.Services.CapsuleService
{
    // Сервис для регистрации капсул в лист и поиска ближайшей капсулы
    public class RegisterService : IRegisterService
    {
        private readonly List<CapsuleController> _friendlyCapsules = new List<CapsuleController>();
        private readonly List<CapsuleController> _enemyCapsules = new List<CapsuleController>();
        
        private readonly List<SphereController> _frinedlySpheres = new List<SphereController>();
        private readonly List<SphereController> _enemySpheres = new List<SphereController>();

        public void RegisterCapsule(CapsuleController capsule)
        {
            if (capsule.EntityType == EntityType.Friendly)
                _friendlyCapsules.Add(capsule);
            else
                _enemyCapsules.Add(capsule);
        }

        public void UnregisterCapsule(CapsuleController capsule)
        {
            if (capsule.EntityType == EntityType.Friendly)
                _friendlyCapsules.Remove(capsule);
            else
                _enemyCapsules.Remove(capsule);
        }
        
        public void RegisterSphere(SphereController sphere)
        {
            if (sphere.entityType == EntityType.Friendly)
                _frinedlySpheres.Add(sphere);
            else
                _enemySpheres.Add(sphere);
        }

        public void UnregisterSphere(SphereController sphere)
        {
            if (sphere.entityType == EntityType.Friendly)
                _frinedlySpheres.Remove(sphere);
            else
                _enemySpheres.Remove(sphere);
        }

        public CapsuleController FindNearestTarget(EntityType entityType, Vector3 position)
        {
            var targetList = (entityType == EntityType.Friendly) ? _enemyCapsules : _friendlyCapsules;
            var validTargets = targetList
                .Where(c => c != null && c.gameObject.activeInHierarchy)
                .ToList();

            if (validTargets.Count == 0)
                return null;

            return validTargets
                .OrderBy(c => Vector3.Distance(position, c.transform.position))
                .FirstOrDefault();
        }
        
        public SphereController FindNearestSphere(EntityType entityType, Vector3 position)
        {
            var targetList = (entityType == EntityType.Friendly) ? _enemySpheres : _frinedlySpheres;
            var validTargets = targetList
                .Where(c => c != null && c.gameObject.activeInHierarchy)
                .ToList();

            if (validTargets.Count == 0)
                return null;

            return validTargets
                .OrderBy(c => Vector3.Distance(position, c.transform.position))
                .FirstOrDefault();
        }
    }
}