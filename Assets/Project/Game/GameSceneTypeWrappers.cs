using DenZ.DevelopmentTools.Di;
using UnityEngine;

namespace HoaR.Game
{
    public class OriginPosition : TypeWrapper<Transform> { public OriginPosition(Transform value) : base(value) { } }
    public class DestinationPosition : TypeWrapper<Transform> { public DestinationPosition(Transform value) : base(value) { } }
    public class TrackedPosition : TypeWrapper<Transform> { public TrackedPosition(Transform value) : base(value) { } }
}