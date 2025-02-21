using System.Linq;
using ProjectDawn.Navigation.Hybrid;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Models;
using WhoIsBigger.Scripts.Services.CapsuleService;
using Zenject;

namespace WhoIsBigger.Scripts.Views.Capsule
{
    public class CapsuleController : MonoBehaviour
    {
        [Inject] private EventManager _eventManager;
        [Inject] private IRegisterService _registerService;
        
        private AgentAuthoring _agent;
        private string _tagToChase;
        
        private EntityType _entityType;
        public EntityType EntityType => _entityType;
        
        public int health;
        public int damage;
        public float impulseDistance;
        public bool isDead = false;
        
        private Vector3 _velocityThis;
        private Vector3 _velocityOther;
        
        private HealthBar _healthBar;
        
        // Construct вызывается вручную в CapsuleFactory
        public void Construct(EntityType type, Vector3 transform)
        {
            gameObject.transform.position = transform;
            _entityType = type;
        }
        
        private void Awake()
        {
            _healthBar = GetComponentInChildren<HealthBar>();
            _agent = GetComponent<AgentAuthoring>();
            if (_agent.IsUnityNull())
            {
                Debug.LogError("No agent found");
            }

            health = 300;
            damage = Random.Range(10, 50);
            impulseDistance = Random.Range(100, 200);
        }

        private void Start()
        {
            // Регистрируем капсулу 
            _registerService.RegisterCapsule(this);
            
            // Устанавливаем тег для преследования
            if (_entityType == EntityType.Friendly)
            {
                _tagToChase = EntityTag.Enemy;
            }
            else if (_entityType == EntityType.Enemy)
            {
                _tagToChase = EntityTag.Friendly;
            }
            else
            {
                Debug.LogError("Unknown type " + gameObject.name);
            }
            Debug.Log("Chasing " + _tagToChase);
            
            if (_healthBar != null)
            {
                _healthBar.SetHealth(health);
            }
        }

        private void Update()
        {
            if (_agent == null)
                return;

            var targetCapsule = _registerService.FindNearestTarget(_entityType, transform.position);
            if (targetCapsule != null)
            {
                _agent.SetDestination(targetCapsule.transform.position);
            }
            else
            {
                var targetSphere = _registerService.FindNearestSphere(_entityType, transform.position);
                if (targetSphere != null)
                {
                    _agent.SetDestination(targetSphere.transform.position);
                }
                else
                {
                    _agent.Stop();
                }
            }               
        }
        
        public void HandleFight(Collider collider)
        {
            var otherCapsule = collider.GetComponentInParent<CapsuleController>();
            if(otherCapsule == null) { return; }
            
            // Проверка на френдли фаер
            if (_entityType == otherCapsule.EntityType)
                return;
            
            otherCapsule.TakeDamage(this.damage);
            ApplyImpulse(otherCapsule.transform);
        }
        
        public void DestroySphere(Collider collider)
        {
            var otherCapsule = collider.GetComponentInParent<SphereController>();
            if (_entityType == otherCapsule.entityType)
                return;
            
            Destroy(collider.gameObject);
        }
        
        public void TakeDamage(int getDamage)
        {
            if (isDead)
                return;
            
            health -= getDamage;
            _healthBar.SetHealth(health);
            
            if (health <= 0)
            {
                isDead = true;
                _eventManager.OnUnitDie.Invoke(this);
            }
            Debug.Log("I TOOK DAMAGE IT HURTS " + damage);
        }
        
        // Some ChatGpt code
        private void ApplyImpulse(Transform other)
        {
            float smoothTime = 1.5f; // Чем меньше, тем быстрее (и резче) движение

            // Вычисляем направление от другого объекта к этому
            Vector3 direction = (transform.position - other.position).normalized;
    
            // Целевые позиции для плавного смещения
            Vector3 targetPosition = transform.position + direction * impulseDistance;
            Vector3 otherTargetPosition = other.position - direction * impulseDistance;
    
            // Плавное перемещение с использованием SmoothDamp
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocityThis, smoothTime);
            other.position = Vector3.SmoothDamp(other.position, otherTargetPosition, ref _velocityOther, smoothTime);
        }
        
        private void OnDestroy()
        {
            // разрегистрируем капсулу
            _registerService.UnregisterCapsule(this);
        }
    }
}