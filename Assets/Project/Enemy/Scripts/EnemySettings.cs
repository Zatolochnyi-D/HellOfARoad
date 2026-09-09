using HoaR.HealthSystem;
using UnityEngine;

namespace HoaR.Enemies
{
    [CreateAssetMenu(fileName = nameof(EnemySettings), menuName = "Project/Enemy/" + nameof(EnemySettings))]
    public class EnemySettings : ScriptableObject, IHealthSettingsProvider
    {
        [SerializeField] private int _maxHealthPoints;
        [SerializeField] private float _angeringDistance;

        public int MaxHealthPoints => _maxHealthPoints;
        public float AngeringDistance => _angeringDistance;
    }
}