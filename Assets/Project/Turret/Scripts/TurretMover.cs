using DenZ.DevelopmentTools.Utilities;
using UnityEngine;

namespace HoaR.Turret
{
    public class TurretMover
    {
        private readonly Transform _turretTransform;

        private float _currentRotation = 0f;

        public TurretMover(Transform turretTransform)
        {
            _turretTransform = turretTransform;

            _ = Timers.InvokeEachFrameIndefinitely(() =>
            {
                _currentRotation += Time.deltaTime * 10f;
                var rot = Mathf.PingPong(_currentRotation, 140f) - 70f;
                _turretTransform.eulerAngles = new(0f, rot, 0f);
            });
        }
    }
}