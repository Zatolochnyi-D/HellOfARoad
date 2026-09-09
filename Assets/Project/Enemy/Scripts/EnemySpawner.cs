using System.Linq;
using DenZ.DevelopmentTools.Extensions;
using HoaR.Game.LevelManagement;
using HoaR.LevelManagement;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace HoaR.Enemies
{
    public class EnemySpawner
    {
        private static (float, float) GetLengthRangeForSegment(int segment, int segmentsCount, float length)
        {
            var lengthPerSegment = length / (segmentsCount - 1);
            var lengthPerHalfSegment = lengthPerSegment / 2f;
            return segment switch
            {
                var x when x == 0 => (0f, lengthPerHalfSegment),
                var x when x == segmentsCount - 1 => (length - lengthPerHalfSegment, length),
                _ => (lengthPerHalfSegment + lengthPerSegment * (segment - 1), lengthPerHalfSegment + lengthPerSegment * segment),
            };
        }

        private static Vector2 GetPointInSquare((float, float) lengthRange, float width, System.Random random)
        {
            return new(
                Mathf.Lerp(-width / 2f, width / 2f, (float)random.NextDouble()),
                Mathf.Lerp(lengthRange.Item1, lengthRange.Item2, (float)random.NextDouble())
            );
        }

        private readonly LevelSettings _levelSettings;
        private readonly Transform _levelOrigin;
        private readonly IFactory<GameObject, Vector3, float, Enemy> _enemyFactory;

        public EnemySpawner(LevelSettings levelSettings, LevelOrigin levelOrigin, IFactory<GameObject, Vector3, float, Enemy> enemyFactory)
        {
            _levelSettings = levelSettings;
            _levelOrigin = levelOrigin.Value;
            _enemyFactory = enemyFactory;

            SpawnAll();
        }

        private void SpawnAll()
        {
            var randomizer = new System.Random(_levelSettings.RandomizationSeed);
            _levelSettings.EnemySpawningSettings.ForEach(x =>
            {
                Enumerable.Range(0, _levelSettings.SegmentsCount)
                          .Select(y =>
                          {
                              var step = 1f / (_levelSettings.SegmentsCount - 1);
                              var actualEnemyCountPerSegment = Mathf.RoundToInt(x.EnemySpawnRatePerSegment.Evaluate(step * y) * x.MaxEnemyAmount);
                              var rangeForThatSegment = GetLengthRangeForSegment(y, _levelSettings.SegmentsCount, _levelSettings.LevelLength);
                              return (actualEnemyCountPerSegment, rangeForThatSegment);
                          })
                          .SelectMany(y =>
                          {
                              return Enumerable.Range(0, y.actualEnemyCountPerSegment)
                                               .Select(z => GetPointInSquare(y.rangeForThatSegment, _levelSettings.SpawnAreaWidth, randomizer));
                          })
                          .ForEach(y =>
                          {
                              var vector3d = y.AsX0Y().With(y: _levelOrigin.position.y);
                              var spawnPoint = _levelOrigin.worldToLocalMatrix.MultiplyPoint(vector3d);
                              var rotation = (float)randomizer.NextDouble() * 360f;
                              _enemyFactory.Create(x.EnemyPrefab, spawnPoint, rotation);
                          });
            });
        }
    }
}
