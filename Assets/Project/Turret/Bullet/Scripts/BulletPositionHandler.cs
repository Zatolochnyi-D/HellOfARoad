using UnityEngine;

namespace HoaR.Turret.Shooting
{
    public class BulletPositionHandler
    {
        private readonly Transform _bulletTransform;
        private readonly BulletSettings _settings;
        private readonly Rigidbody _rigidbody;

        public BulletPositionHandler(Transform bulletTransform, BulletSettings settings, Rigidbody rigidbody)
        {
            _bulletTransform = bulletTransform;
            _settings = settings;
            _rigidbody = rigidbody;

            _bulletTransform.parent = null;
        }

        public void Spawn(Transform spawnPosition)
        {
            _bulletTransform.position = spawnPosition.position;
            _bulletTransform.forward = spawnPosition.forward;
        }

        public void StartFly()
        {
            _rigidbody.linearVelocity = _settings.FlyingSpeed * _bulletTransform.forward;
        }

        public void StopFly()
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }
    }
}