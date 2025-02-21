﻿using UnityEngine;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Views.Capsule;

namespace WhoIsBigger.Scripts.Services.SpawnService
{
    public interface ISpawnService
    {
        CapsuleController SpawnCapsule(CapsuleType type, Vector3 position);
        void DestroyCapsule(CapsuleController capsule);
    }
}