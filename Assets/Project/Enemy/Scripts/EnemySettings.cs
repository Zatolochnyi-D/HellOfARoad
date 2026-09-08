using HoaR.HealthSystem;
using UnityEngine;

namespace HoaR.Enemies
{
    [CreateAssetMenu(fileName = nameof(EnemySettings), menuName = "Project/Enemy/" + nameof(EnemySettings))]
    public class EnemySettings : ScriptableObject, IHealthSettingsProvider
    {
        [SerializeField] private int _maxHealthPoints;

        public int MaxHealthPoints => _maxHealthPoints;
    }
}