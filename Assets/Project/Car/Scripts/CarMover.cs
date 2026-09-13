using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Threading;
using DenZ.DevelopmentTools.Utilities;
using DG.Tweening;
using UnityEngine;

namespace HoaR.Car
{
    public class CarMover
    {
        private readonly Transform _carTransform;
        private readonly CarSettings _carSettings;
        private readonly CancellationToken _destroyCancellationToken;

        private CancellationTokenSource _movementCycleCancellation;

        public CarMover(Transform carTransform, CarSettings carSettings, CancellationToken destroyCancellationToken)
        {
            _carTransform = carTransform;
            _carSettings = carSettings;
            _destroyCancellationToken = destroyCancellationToken;
        }

        private void MoveForward(float speed)
        {
            _carTransform.position += Time.deltaTime * speed * _carSettings.GeneralMovementVector;
        }

        private void MoveForward(Func<float> speedGetter)
        {
            _carTransform.position += Time.deltaTime * speedGetter() * _carSettings.GeneralMovementVector;
        }

        public void Enable()
        {
            _movementCycleCancellation = new();
            _ = Timers.InvokeEachFrameIndefinitely(() => MoveForward(_carSettings.MovementSpeed), _movementCycleCancellation.Token, true);
        }

        public void Disable()
        {
            _movementCycleCancellation?.Cancel();
            _movementCycleCancellation = null;
        }

        public void StopGradually()
        {
            var currentSpeed = _carSettings.MovementSpeed;
            var smoothStopTween = DOTween.To(() => currentSpeed, value => currentSpeed = value, 0f, _carSettings.TimeToStopCarAfterGameEnd).OnUpdate(() => MoveForward(() => currentSpeed));
            _destroyCancellationToken.Register(() =>
            {
                if (smoothStopTween.IsActive())
                    smoothStopTween.Kill();
            });
        }
    }
}