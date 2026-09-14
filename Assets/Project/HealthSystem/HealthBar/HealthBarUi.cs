using System.Collections.Generic;
using DG.Tweening;
using HoaR.HealthSystem.Damage;
using HoaR.Utilities;
using UnityEngine;
using Zenject;

namespace HoaR.HealthSystem.HealthComponent
{
    public class HealthBarUi : MonoBehaviour
    {
        [SerializeField] private ProgressBarMask _fillableBar;
        [Inject] private readonly Health _health;
        [Inject] private readonly HealthBarTweeningSettings _settings;

        private readonly List<Tweener> _tweenersToKill = new();

        void Start()
        {
            _health.OnDamageReceived += HandleDamageReceived;
            _fillableBar.SetFill(1f);
        }

        void OnDestroy()
        {
            _tweenersToKill.ForEach(x => x.Kill());
            _health.OnDamageReceived -= HandleDamageReceived;
        }

        private void HandleDamageReceived(DamageReceivedInfo info)
        {
            var tween = DOTween.To(() => _fillableBar.FillAmount, fill => _fillableBar.SetFill(fill), info.NormalizedHealthLeft, _settings.TimeForOneTween).SetEase(Ease.OutCirc);
            _tweenersToKill.Add(tween);
            tween.OnComplete(() => _tweenersToKill.Remove(tween));
        }
    }
}
