using HoaR.HealthSystem.DamageDealing;
using UnityEngine;

namespace HoaR.Turret.Shooting
{
    [CreateAssetMenu(fileName = nameof(BulletSettings), menuName = "Project/Bullet/" + nameof(BulletSettings))]
    public class BulletSettings : ScriptableObject, IDamageDealerSettings
    {
        [SerializeField] private float _flyingSpeed = 10f;
        [SerializeField] private float _timeBeforeDespawn = 7f;
        [SerializeField] private int _damage = 34;

        public float FlyingSpeed => _flyingSpeed;
        public float TimeBeforeDespawn => _timeBeforeDespawn;
        public int Damage => _damage;
    }
}