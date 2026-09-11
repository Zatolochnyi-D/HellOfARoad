using System;
using DenZ.DevelopmentTools.Di;
using DenZ.DevelopmentTools.Options;
using HoaR.HealthSystem.HealthComponent;
using UnityEngine;
using Zenject;

namespace HoaR.HealthSystem.DamageDealing
{
    public class DamageDealerMb : MonoBehaviour, IDamageDealer
    {
        public event Action OnHitTarget;
        public event Action OnKill;

        [Inject] private readonly IDamageDealerSettings _settings;

        private bool _active = true;

        void OnTriggerEnter(Collider other)
        {
            if (!_active)
                return;
            if (((1 << other.gameObject.layer) & _settings.TriggerOn.value) == 0)
                return;
            var component = other.gameObject.FindFromContainerOnObjectOrItsParents<IDamageReceiver>();
            component.Apply(x =>
            {
                var isKill = false;
                if (_settings.IsRelative)
                    isKill = x.ReceiveRelativeDamage(_settings.RelativeDamage); 
                else
                    isKill = x.ReceiveAbsoluteDamage(_settings.Damage);
                OnHitTarget?.Invoke();
                if (isKill)
                    OnKill?.Invoke();
            });
        }

        public void Activate()
        {
            _active = true;
        }

        public void Deactivate()
        {
            _active = false;
        }
    }
}
