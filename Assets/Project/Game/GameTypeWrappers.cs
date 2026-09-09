using DenZ.DevelopmentTools.Di;
using HoaR.Enemies;
using UnityEngine;

namespace HoaR.Game
{
    public class CarTransform : TypeWrapper<Transform>, IAngerTrackable
    { public CarTransform(Transform value) : base(value) { } }
}