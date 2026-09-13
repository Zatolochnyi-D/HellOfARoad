using DG.Tweening;
using HoaR.Utilities;
using UnityEngine;
using Zenject;

namespace HoaR.HealthSystem.HealthComponent
{
    public class HealthBarUi : MonoBehaviour
    {
        [SerializeField] private ProgressBarMask _fillableBar;
        [Inject] private readonly Health _health;

        void Start()
        {
            _health.OnDamageReceived += HandleDamageReceived;
            _fillableBar.SetFill(1f);
        }

        void OnDestroy()
        {
            _health.OnDamageReceived -= HandleDamageReceived;
        }

        private void HandleDamageReceived(float normalizedHealth)
        {
            DOTween.To(() => _fillableBar.FillAmount, fill => _fillableBar.SetFill(fill), normalizedHealth, 0.25f).SetEase(Ease.OutCirc);
        }
    }
}
