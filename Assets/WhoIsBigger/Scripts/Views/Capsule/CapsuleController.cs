using System.Linq;
using ProjectDawn.Navigation.Hybrid;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Models;
using WhoIsBigger.Scripts.Services;
using Zenject;

namespace WhoIsBigger.Scripts.Views.Capsule
{
    public class CapsuleController : MonoBehaviour
    {
        
        private CapsuleType _capsuleType;
        
        [Inject] private EventManager _eventManager;
        private AgentAuthoring _agent;
        private string _tagToChase;
        
        public CapsuleType CapsuleType => _capsuleType;
        
        // Construct вызывается вручную в CapsuleFactory
        public void Construct(CapsuleType type, Vector3 transform)
        {
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
                Debug.LogError("Unknown type " + gameObject.name);
            }
            Debug.Log("Chasing " + _tagToChase);
        }

        private void OnDestroy()
        {
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
                _agent.SetDestination(target.transform.position );
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
    
            var validTargets = targetList.Where(c => c != null && c.gameObject.activeInHierarchy).ToList();
            if (validTargets.Count == 0)
                return null;

            CapsuleController nearest = validTargets
                .OrderBy(c => Vector3.Distance(transform.position, c.transform.position))
                .First();
            return nearest.gameObject;
        }
        
        public void HandleFight(Collider collider)
        {
            var otherCapsule = collider.GetComponentInParent<CapsuleController>();
            if(otherCapsule == null)
            {
                Debug.LogWarning("Другой объект не имеет CapsuleController");
                return;
            }
            
            // Убираем задваивание
            if (this.GetInstanceID() < otherCapsule.GetInstanceID())
            {
                _eventManager.OnUnitDie.Invoke(this);
                _eventManager.OnUnitDie.Invoke(otherCapsule);
            }
        }

    }
}