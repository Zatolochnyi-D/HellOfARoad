using DenZ.DevelopmentTools.Di;
using HoaR.Enemies;
using HoaR.GoalChecking;
using UnityEngine;

namespace HoaR.Game
{
    public class CarTransform : TypeWrapper<Transform>, IEnemyTarget, ICarTransformProvider
    {
        public CarTransform(Transform value) : base(value) { }

        public Vector3 Position => Value.position;
    }
}