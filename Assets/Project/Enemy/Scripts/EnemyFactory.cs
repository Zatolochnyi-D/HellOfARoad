using System.ComponentModel;
using DenZ.DevelopmentTools.Di;
using UnityEngine;
using Zenject;

namespace HoaR.Enemies
{
    public class EnemyFactory : IFactory<GameObject, Vector3, Enemy>
    {
        private readonly DiContainer _container;

        public EnemyFactory(DiContainer container)
        {
            _container = container;
        }

        public Enemy Create(GameObject enemyPrefab, Vector3 spawnPoint)
        {
            var enemy = _container.InstantiatePrefab(enemyPrefab);
            enemy.transform.position = spawnPoint;

            return enemy.GetFromContainer<Enemy>();
        }
    }
}