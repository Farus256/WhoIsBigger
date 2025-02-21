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
            if (other.CompareTag(CapsuleTag.Friendly) || other.CompareTag(CapsuleTag.Enemy))
            {
                _controller.HandleFight(other, false);
            }
        }
    }
}