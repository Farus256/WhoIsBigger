using System.Linq;
using ProjectDawn.Navigation.Hybrid;
using Unity.VisualScripting;
using UnityEngine;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Models;
using WhoIsBigger.Scripts.Services.CapsuleService;
using Zenject;

namespace WhoIsBigger.Scripts.Views.Capsule
{
    public class CapsuleController : MonoBehaviour
    {
        [Inject] private EventManager _eventManager;
        [Inject] private ICapsuleService _capsuleService;
        
        private AgentAuthoring _agent;
        private string _tagToChase;
        
        private CapsuleType _capsuleType;
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
            // Регистрируем капсулу 
            _capsuleService.RegisterCapsule(this);
            
            // Устанавливаем тег для преследования
            if (_capsuleType == CapsuleType.Friendly)
            {
                _tagToChase = CapsuleTag.Enemy;
            }
            else if (_capsuleType == CapsuleType.Enemy)
            {
                _tagToChase = CapsuleTag.Friendly;
            }
            else
            {
                Debug.LogError("Unknown type " + gameObject.name);
            }
            Debug.Log("Chasing " + _tagToChase);
        }

        private void Update()
        {
            if (_agent == null)
                return;
            
            var target = _capsuleService.FindNearestTarget(_capsuleType, transform.position);
            if (target != null)
            {
                _agent.SetDestination(target.transform.position );
            }
            else
            {
                _agent.Stop();
            }               
        }
        
        public void HandleFight(Collider collider)
        {
            var otherCapsule = collider.GetComponentInParent<CapsuleController>();
            if(otherCapsule == null) { return; }
            
            // Проверка на френдли фаер
            if (_capsuleType == otherCapsule.CapsuleType)
                return;
            
            // Убираем задваивание
            if (this.GetInstanceID() < otherCapsule.GetInstanceID())
            {
                _eventManager.OnCapsuleCollide.Invoke(this, otherCapsule);
            }
        }
        
        private void OnDestroy()
        {
            // разрегистрируем капсулу
            _capsuleService.UnregisterCapsule(this);
        }
    }
}