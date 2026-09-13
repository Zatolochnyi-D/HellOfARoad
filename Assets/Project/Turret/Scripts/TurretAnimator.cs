using System;
using UnityEngine;

namespace HoaR.Turret
{
    public class TurretAnimator : IDisposable
    {
        private static readonly int SHOOT_ANIMATION_HASH = Animator.StringToHash("Shoot");

        private readonly Animator _animator;
        private readonly TurretShooterSettings _settings;
        private readonly TurretShooter _shooter;

        public TurretAnimator(Animator animator, TurretShooterSettings settings, TurretShooter shooter)
        {
            _animator = animator;
            _settings = settings;
            _shooter = shooter;

            _shooter.OnShoot += PlayShootAnimation;
        }

        private void PlayShootAnimation()
        {
            _animator.speed = _settings.FireRate;
            _animator.Play(SHOOT_ANIMATION_HASH, 0, 0f);
        }

        public void Dispose()
        {
            _shooter.OnShoot -= PlayShootAnimation;
        }
    }
}