using UnityEngine;

namespace HoaR.Turret.Shooting
{
    [CreateAssetMenu(fileName = nameof(BulletSettings), menuName = "Project/Bullet/" + nameof(BulletSettings))]
    public class BulletSettings : ScriptableObject
    {
        [SerializeField] private float _flyingSpeed = 10f;
        [SerializeField] private float _timeBeforeDespawn = 7f;

        public float FlyingSpeed => _flyingSpeed;
        public float TimeBeforeDespawn => _timeBeforeDespawn;
    }
}