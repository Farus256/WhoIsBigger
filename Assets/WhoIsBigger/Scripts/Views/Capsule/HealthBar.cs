using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    private Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    public void SetHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }
    
    private void LateUpdate()
    {
        if (_mainCamera != null)
        {
            // Делаем из полоски здоровья биллборд
            transform.rotation = Quaternion.LookRotation(transform.position - _mainCamera.transform.position);
        }
    }
}