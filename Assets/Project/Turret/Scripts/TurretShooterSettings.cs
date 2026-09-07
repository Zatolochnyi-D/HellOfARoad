using UnityEngine;

namespace HoaR.Turret
{
    [CreateAssetMenu(fileName = nameof(TurretShooterSettings), menuName = "Project/Turret/" + nameof(TurretShooterSettings))]
    public class TurretShooterSettings : ScriptableObject
    {
        [SerializeField] private float _fireRate = 5f;

        public float FireRate => _fireRate;
        public float TimeToShoot => 1f / _fireRate;
    }
}