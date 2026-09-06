using UnityEngine;
using Zenject;

namespace HoaR.Turret.Shooting
{
    public class Bullet : IPoolable<Transform, IMemoryPool>
    {
        private readonly Transform _bulletTransform;

        private IMemoryPool _parentPool;

        public Bullet(Transform bulletTransform)
        {
            _bulletTransform = bulletTransform;

            _bulletTransform.parent = null;
            _bulletTransform.gameObject.SetActive(false);
        }

        public void OnSpawned(Transform spawnPosition, IMemoryPool pool)
        {
            _parentPool = pool;
            _bulletTransform.position = spawnPosition.position;
            _bulletTransform.forward = spawnPosition.forward;
        }

        public void OnDespawned()
        {
            Debug.Log("Despawned");
        }
    }
}