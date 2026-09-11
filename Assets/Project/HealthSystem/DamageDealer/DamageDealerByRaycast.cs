using System;
using System.Threading;
using DenZ.DevelopmentTools.Di;
using DenZ.DevelopmentTools.Options;
using DenZ.DevelopmentTools.Utilities;
using HoaR.HealthSystem.HealthComponent;
using UnityEngine;

namespace HoaR.HealthSystem.DamageDealing
{
    public class DamageDealerByRaycast : IDamageDealer
    {
        private static readonly RaycastHit[] RAYCAST_BUFFER = new RaycastHit[1];

        public event Action OnHitTarget;
        public event Action OnKill;

        private readonly Transform _selfTransform;
        private readonly IDamageDealerSettings _settings;

        private CancellationTokenSource _raycastLoopCancellation;
        private bool _hitWasDone = false;

        public DamageDealerByRaycast(Transform selfTransform, IDamageDealerSettings settings)
        {
            _selfTransform = selfTransform;
            _settings = settings;
        }

        private void LookForToDoDamage()
        {
            if (_hitWasDone)
                return;

            var hits = Physics.RaycastNonAlloc(_selfTransform.position, _selfTransform.forward, RAYCAST_BUFFER, 80f * Time.fixedDeltaTime, _settings.TriggerOn);

            if (hits != 0)
            {
                var hit = RAYCAST_BUFFER[0];
                hit.collider.gameObject.FindFromContainerOnObjectOrItsParents<IDamageReceiver>().Apply(x =>
                {
                    _hitWasDone = true;
                    _ = Timers.InvokeOnce(() =>
                    {
                        var isKill = false;
                        if (_settings.IsRelative)
                            isKill = x.ReceiveRelativeDamage(_settings.RelativeDamage);
                        else
                            isKill = x.ReceiveAbsoluteDamage(_settings.Damage);
                        OnHitTarget?.Invoke();
                        if (isKill)
                            OnKill?.Invoke();
                        _hitWasDone = false;
                    }, 1);
                });
            }
        }

        private async void RaycastLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                LookForToDoDamage();
                await Awaitable.FixedUpdateAsync();
            }
        }

        public void Activate()
        {
            _raycastLoopCancellation = new();
            RaycastLoop(_raycastLoopCancellation.Token);
        }

        public void Deactivate()
        {
            _raycastLoopCancellation?.Cancel();
            _raycastLoopCancellation = null;
        }
    }
}
