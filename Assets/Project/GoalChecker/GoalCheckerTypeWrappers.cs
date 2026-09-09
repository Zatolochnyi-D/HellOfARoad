using DenZ.DevelopmentTools.Di;
using UnityEngine;

namespace HoaR.GoalChecking
{
    public class OriginPosition : TypeWrapper<Transform> { public OriginPosition(Transform value) : base(value) { } }

    public interface ICarTransformProvider
    {
        public Transform Value { get; }
    }
}