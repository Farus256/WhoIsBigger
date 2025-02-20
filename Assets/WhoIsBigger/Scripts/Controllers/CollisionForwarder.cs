using UnityEngine;
using UnityEngine.Rendering;

namespace WhoIsBigger.Scripts.View
{
    public class CollisionForwarder : MonoBehaviour
    {
        private CapsuleController _controller;
        private void Awake()
        {
            _controller = GetComponentInParent<CapsuleController>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            _controller.OnCollisionEnter(collision);
        }
    }
}