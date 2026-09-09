using System;
using DenZ.DevelopmentTools.Di;
using DenZ.DevelopmentTools.Options;
using HoaR.HealthSystem;
using UnityEngine;
using Zenject;

namespace HoaR.Turret.Shooting
{
    public class BulletDamageDealer : MonoBehaviour
    {
        public event Action OnBulletHitTarget;

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

        [Inject] private readonly BulletSettings _settings;

        void OnTriggerEnter(Collider collider)
        {
            var component = FindComponentOnObjectOrItsParents<IDamageReceiver>(collider.gameObject);
            component.Apply(x => { x.ReceiveAbsoluteDamage(_settings.Damage); OnBulletHitTarget?.Invoke(); });
        }
    }
}