using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Views.Capsule;

namespace WhoIsBigger.Scripts.Services.CapsuleService
{
    // Сервис для регистрации капсул в лист и поиска ближайшей капсулы
    public class CapsuleService : ICapsuleService
    {
        private readonly List<CapsuleController> _friendlyCapsules = new List<CapsuleController>();
        private readonly List<CapsuleController> _enemyCapsules = new List<CapsuleController>();

        public void RegisterCapsule(CapsuleController capsule)
        {
            if (capsule.CapsuleType == CapsuleType.Friendly)
                _friendlyCapsules.Add(capsule);
            else
                _enemyCapsules.Add(capsule);
        }

        public void UnregisterCapsule(CapsuleController capsule)
        {
            if (capsule.CapsuleType == CapsuleType.Friendly)
                _friendlyCapsules.Remove(capsule);
            else
                _enemyCapsules.Remove(capsule);
        }

        public CapsuleController FindNearestTarget(CapsuleType capsuleType, Vector3 position)
        {
            var targetList = (capsuleType == CapsuleType.Friendly) ? _enemyCapsules : _friendlyCapsules;
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