using HoaR.HealthSystem;
using UnityEngine;

namespace HoaR.Enemies
{
    public class Enemy
    {
        private readonly GameObject _self;

        public Enemy(GameObject self, Health health)
        {
            _self = self;
            health.OnHealthDepleted += () => Object.Destroy(_self);
        }
    }
}