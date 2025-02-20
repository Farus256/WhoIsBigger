using System.Collections.Generic;
using WhoIsBigger.Scripts.View;

namespace WhoIsBigger.Scripts.Controllers.Capsule
{
    public class CapsuleRegistry
    {
        public static readonly List<CapsuleController> FriendlyCapsules = new List<CapsuleController>();
        public static readonly List<CapsuleController> EnemyCapsules = new List<CapsuleController>();
    }
}