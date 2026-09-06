using HoaR.InputManagement;
using UnityEngine;

namespace HoaR.Turret
{
    public class TurretMover
    {
        private readonly Transform _turretTransform;
        private readonly TurretMoverSettings _settings;

        private float _currentRotationValue = 0f;

        public TurretMover(Transform turretTransform, PlayerInputInterceptor playerInputInterception, TurretMoverSettings settings)
        {
            _turretTransform = turretTransform;
            _settings = settings;

            playerInputInterception.OnHorizontalSwipe += HandleVerticalSwipe;
        }

        private void HandleVerticalSwipe(float relativeDelta)
        {
            var rotationValue = relativeDelta * 2f * _settings.MaxDeviation / _settings.RelativeDistanceForMaxEffect;
            _currentRotationValue = Mathf.Clamp(_currentRotationValue + rotationValue, -_settings.MaxDeviation, _settings.MaxDeviation);
            _turretTransform.eulerAngles = new(0f, _currentRotationValue, 0f);
        }
    }
}