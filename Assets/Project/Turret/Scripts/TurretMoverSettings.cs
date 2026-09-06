using UnityEngine;

namespace HoaR.Turret
{
    [CreateAssetMenu(fileName = nameof(TurretMoverSettings), menuName = "Project/Turret/" + nameof(TurretMoverSettings))]
    public class TurretMoverSettings : ScriptableObject
    {
        [SerializeField] private float _maxDeviation = 80f;
        [SerializeField] private float _relativeDistanceForMaxEffect = 0.5f; // How much % of screen finger should swipe to make turret rotate by 2x 
                                                                             // of deviation value.
        public float MaxDeviation => _maxDeviation;
        public float RelativeDistanceForMaxEffect => _relativeDistanceForMaxEffect;
    }
}