using System.Collections.Generic;
using WhoIsBigger.Scripts.Views.Capsule;

namespace WhoIsBigger.Scripts.Models
{
    public static class CapsuleRegistry
    {
        public static readonly List<CapsuleController> FriendlyCapsules = new List<CapsuleController>();
        public static readonly List<CapsuleController> EnemyCapsules = new List<CapsuleController>();
    }
}