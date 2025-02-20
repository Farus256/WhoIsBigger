using System;
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
            // Регистрируем капсулу в реестре в зависимости от её типа
            if (_capsuleType == CapsuleType.Friendly)
            {
                CapsuleRegistry.FriendlyCapsules.Add(this);
                _tagToChase = CapsuleTag.Enemy;
            }
            else if (_capsuleType == CapsuleType.Enemy)
            {
                CapsuleRegistry.EnemyCapsules.Add(this);
                _tagToChase = CapsuleTag.Friendly;
            }
            else
            {
                Debug.LogError("Неизвестный тип капсулы на " + gameObject.name);
            }
            Debug.Log("догоняю " + _tagToChase);
        }

        private void OnDestroy()
        {
            // Удаляем капсулу из реестра, когда она отключается
            if (_capsuleType == CapsuleType.Friendly)
                CapsuleRegistry.FriendlyCapsules.Remove(this);
            else if (_capsuleType == CapsuleType.Enemy)
                CapsuleRegistry.EnemyCapsules.Remove(this);
        }

        private void Update()
        {
            if (_agent == null)
                return;
            
            var target = FindNearestTarget();
            if (target != null)
            {
                _agent.SetDestination(target.transform.position + new Vector3(0.2f, 0, 0));
            }
            else
            {
                _agent.Stop();
            }               
        }
        
        private GameObject FindNearestTarget()
        {
            var targetList = _capsuleType == CapsuleType.Friendly 
                ? CapsuleRegistry.EnemyCapsules 
                : CapsuleRegistry.FriendlyCapsules;

            if (targetList.Count == 0)
                return null;

            CapsuleController nearest = targetList
                .OrderBy(c => Vector3.Distance(transform.position, c.transform.position))
                .First();
            return nearest.gameObject;
        }

        public void HandleFight(Collision collision)
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