using UnityEngine;
using WhoIsBigger.Scripts.Common;

namespace WhoIsBigger.Scripts.Views.Capsule
{
    public class CollisionForwarder : MonoBehaviour
    {
        private CapsuleController _controller;
        private void Start()
        {
            _controller = GetComponentInParent<CapsuleController>();
        }

        private void OnTriggerEnter (Collider other)
        {
            if (other.CompareTag(EntityTag.Friendly) || other.CompareTag(EntityTag.Enemy))
            {
                _controller.HandleFight(other);
            }
            else if(other.CompareTag(EntityTag.SphereEnemy) || other.CompareTag(EntityTag.SphereFriendly))
            {
                _controller.DestroySphere(other);
            }
        }
    }
}