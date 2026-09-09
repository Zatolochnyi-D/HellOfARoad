using System;
using DenZ.DevelopmentTools.Di;
using DenZ.DevelopmentTools.Options;
using HoaR.HealthSystem.HealthComponent;
using UnityEngine;
using Zenject;

namespace HoaR.HealthSystem.DamageDealing
{
    public class DamageDealerMb : MonoBehaviour
    {
        public event Action OnHitTarget;
        public event Action OnKill;

        private static Option<T> FindComponentOnObjectOrItsParents<T>(GameObject gameObject) where T : class
        {
            var currentObject = gameObject;
            while (currentObject != null)
            {
                var component = currentObject.TryGetFromPossibleContainerless<T>();
                if (component.IsSome)
                    return component.ValueUnsafe;
                else
                    currentObject = currentObject.transform.parent != null ? currentObject.transform.parent.gameObject : null;
            }
            return Option.None<T>();
        }

        [SerializeField] private LayerMask _triggerOn;
        [Inject] private readonly IDamageDealerSettings _settings;

        void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & _triggerOn.value) == 0)
                return;
            var component = FindComponentOnObjectOrItsParents<IDamageReceiver>(other.gameObject);
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
    }
}
