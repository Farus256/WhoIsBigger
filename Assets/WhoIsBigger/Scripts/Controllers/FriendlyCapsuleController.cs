using System.Linq;
using ProjectDawn.Navigation;
using UnityEngine;
using ProjectDawn.Navigation.Hybrid;
using Unity.VisualScripting;
using UnityEditor;
using Zenject;

namespace WhoIsBigger.Scripts.View
{
    public class FriendlyCapsuleController : MonoBehaviour
    {
        private AgentAuthoring _agent;
        

        [Inject]
        public void Construct(Vector3 transform)
        {
            gameObject.transform.position = transform;
        }
        
        private void Awake()
        {
            _agent = GetComponent<AgentAuthoring>();
            if (_agent.IsUnityNull())
            {
                Debug.LogError("No agent found");
            }
            
           
            
        }

        private void Start()
        {
            var enemy = FindNearestEnemy();
            if (enemy != null)
            {
                _agent.SetDestination(enemy.transform.position + new Vector3(0.1f, 0, 0));
            }
        }

        private GameObject FindNearestEnemy()
        {
            var enemies = GameObject.FindGameObjectsWithTag("EnemyCapsule");
            if(enemies.Length == 0){return null;}
            
            return enemies.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).First();
        }

        private void OnCollisionEnter(Collision  collision)
        {
            Debug.Log("OnCollisionEnter");
            if (collision.gameObject.CompareTag("EnemyCapsule"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}