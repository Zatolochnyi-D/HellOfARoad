using HoaR.HealthSystem.DamageDealing;
using HoaR.HealthSystem.HealthComponent;
using UnityEngine;

namespace HoaR.Enemies
{
    [CreateAssetMenu(fileName = nameof(EnemySettings), menuName = "Project/Enemy/" + nameof(EnemySettings))]
    public class EnemySettings : ScriptableObject, IHealthSettingsProvider, IDamageDealerSettings
    {
        [SerializeField] private int _maxHealthPoints;
        [SerializeField] private float _angeringDistance;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _turnAroundTime; // Time for enemy to rotate by 180°.
        [SerializeField] private int _damageToCar;

        public int MaxHealthPoints => _maxHealthPoints;
        public float AngeringDistance => _angeringDistance;
        public float RunSpeed => _runSpeed;
        public float TurnAroundTime => _turnAroundTime;
        public int Damage => _damageToCar;
    }
}