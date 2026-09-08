using UnityEngine;

namespace HoaR.Game.LevelManagement
{
    [System.Serializable]
    public struct EnemySpawningSetting
    {
        public GameObject EnemyPrefab;
        public AnimationCurve EnemySpawnRatePerSegment;
        public int MaxEnemyAmountPerSegment;
    }

    [CreateAssetMenu(fileName = nameof(LevelSettings), menuName = "Project/Level Management/" + nameof(LevelSettings))]
    public class LevelSettings : ScriptableObject
    {
        [Header("Level Length")]
        [SerializeField] private float _levelLength;

        [Header("Enemy spawning")]
        [SerializeField] private EnemySpawningSetting[] _enemySpawningSettings;
        [SerializeField] private int _segmentsCount; // whole map length is split into this segments, and the middle of animation curve in this segment
                                                     // is used to determine % of max enemy per segment to spawn.
        [SerializeField] private int _randomizationSeed;

        public float LevelLength => _levelLength;
        public EnemySpawningSetting[] EnemySpawningSettings => _enemySpawningSettings;
        public int SegmentsCount => _segmentsCount;
        public int RandomizationSeed => _randomizationSeed;
    }
}
