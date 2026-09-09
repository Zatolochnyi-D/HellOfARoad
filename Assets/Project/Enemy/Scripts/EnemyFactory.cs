using DenZ.DevelopmentTools.Di;
using DenZ.DevelopmentTools.Extensions;
using UnityEngine;
using Zenject;

namespace HoaR.Enemies
{
    public class EnemyFactory : IFactory<GameObject, Vector3, float, Enemy>
    {
        private readonly DiContainer _container;

        public EnemyFactory(DiContainer container)
        {
            _container = container;
        }

        public Enemy Create(GameObject enemyPrefab, Vector3 spawnPoint, float randomizedRotation)
        {
            var enemy = _container.InstantiatePrefab(enemyPrefab);
            enemy.transform.position = spawnPoint;
            enemy.transform.eulerAngles = enemy.transform.eulerAngles.With(y: randomizedRotation);

            return enemy.GetFromContainer<Enemy>();
        }
    }
}