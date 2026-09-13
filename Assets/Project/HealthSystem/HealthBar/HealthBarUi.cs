using HoaR.Utilities;
using UnityEngine;
using Zenject;

namespace HoaR.HealthSystem.HealthComponent
{
    public class HealthBarUi : MonoBehaviour
    {
        [SerializeField] private ProgressBarMask _fillableImage;
        [Inject] private readonly Health _health;

        void Start()
        {
            _health.OnDamageReceived += HandleDamageReceived;
            _fillableImage.SetFill(1f);
        }

        void OnDestroy()
        {
            _health.OnDamageReceived -= HandleDamageReceived;
        }

        private void HandleDamageReceived(float normalizedHealth)
        {
            _fillableImage.SetFill(normalizedHealth);
        }
    }
}
