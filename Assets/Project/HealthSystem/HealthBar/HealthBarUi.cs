using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HoaR.HealthSystem.HealthComponent
{
    public class HealthBarUi : MonoBehaviour
    {
        [SerializeField] private Image _fillableImage;
        [Inject] private readonly Health _health;

        void Start()
        {
            _health.OnDamageReceived += HandleDamageReceived;
            _fillableImage.fillAmount = 1f;
        }
        
        private void HandleDamageReceived(float normalizedHealth)
        {
            _fillableImage.fillAmount = normalizedHealth;
        }
    }
}
