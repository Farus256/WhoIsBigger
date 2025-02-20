using System.Linq;
using ProjectDawn.Navigation;
using UnityEngine;
using ProjectDawn.Navigation.Hybrid;
using Unity.VisualScripting;
using UnityEditor;
using WhoIsBigger.Scripts.Controllers.Capsule;
using Zenject;

namespace WhoIsBigger.Scripts.View
{
    public class CapsuleController : MonoBehaviour
    {
        private AgentAuthoring _agent;
        private CapsuleType _capsuleType;
        private string _tagToChase;
        
        public void Construct(CapsuleType type, Vector3 transform)
        {
            Debug.Log(type + "postavlen");
            gameObject.transform.position = transform;
            _capsuleType = type;
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
            if (_capsuleType == CapsuleType.Friendly)
            {
                _tagToChase = CapsuleTag.Enemy;
            }
            else
            {
                _tagToChase = CapsuleTag.Friendly;
            }
            Debug.Log("догоняю" + _tagToChase);
        }
        
        private void Update()
        {
            if (_agent.IsUnityNull())
            {
                return;
            }
            
            var enemy = FindNearestEnemy();
            if (enemy != null)
            {
                _agent.SetDestination(enemy.transform.position + new Vector3(0.1f, 0, 0));
            }
        }

        private GameObject FindNearestEnemy()
        {
            var enemies = GameObject.FindGameObjectsWithTag(_tagToChase);
            if(enemies.Length == 0){return null;}
            
            return enemies.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).First();
        }

        public void OnCollisionEnter(Collision  collision)
        {
            Debug.Log("OnCollisionEnter");
            if (collision.gameObject.CompareTag(_tagToChase))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}