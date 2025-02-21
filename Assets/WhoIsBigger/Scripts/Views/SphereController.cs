using System;
using UnityEngine;
using UnityEngine.Serialization;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Services.CapsuleService;
using Zenject;

namespace WhoIsBigger.Scripts.Views
{
    public class SphereController : MonoBehaviour
    {
        [Inject] private IRegisterService _registerService;
        public EntityType entityType;

        public void Start()
        {
            _registerService.RegisterSphere(this);
        }

        public void OnDestroy()
        {
            _registerService.UnregisterSphere(this);
        }
    }
}