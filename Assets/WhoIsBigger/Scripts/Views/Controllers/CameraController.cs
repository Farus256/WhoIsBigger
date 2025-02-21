using UnityEngine;

namespace WhoIsBigger.Scripts.Views
{
    public class CameraController : MonoBehaviour
    {
        public float movementSpeed = 5f;

        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            Vector3 movement = new Vector3(horizontal, 0, vertical) * (movementSpeed * Time.deltaTime);
            
            transform.Translate(movement, Space.World);
        }
    }
}