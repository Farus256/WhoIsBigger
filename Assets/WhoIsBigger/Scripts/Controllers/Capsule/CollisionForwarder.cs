using UnityEngine;
using UnityEngine.Rendering;

namespace WhoIsBigger.Scripts.View
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
            _controller.HandleFight(other);
        }
    }
}