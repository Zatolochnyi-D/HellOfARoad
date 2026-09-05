using UnityEngine;

namespace HoaR.Ground
{
    public class GroundExtender
    {
        private readonly (GroundTriggerEnterInterceptor first, GroundTriggerEnterInterceptor second) _groundTriggerPair;

        private readonly Vector3 _startingPosition;
        private readonly Vector3 _extensionDirection;
        private readonly float _extensionDistance;

        private int _extensionCount = 2;

        public GroundExtender((GroundTriggerEnterInterceptor, GroundTriggerEnterInterceptor) groundTriggerPair)
        {
            _groundTriggerPair = groundTriggerPair;

            var (first, second) = _groundTriggerPair;
            _startingPosition = first.GroundTransform.position;
            var vectorDifference = second.GroundTransform.position - first.GroundTransform.position;
            _extensionDirection = vectorDifference.normalized;
            _extensionDistance = vectorDifference.magnitude;

            first.OnCarEnteredTrigger += HandleFirstTriggerEnter;
            second.OnCarEnteredTrigger += HandleSecondTriggerEnter;
        }

        private void HandleFirstTriggerEnter()
        {
            _groundTriggerPair.second.GroundTransform.position = _startingPosition + _extensionDistance * _extensionCount * _extensionDirection;
            _extensionCount++;
        }
        
        private void HandleSecondTriggerEnter()
        {
            _groundTriggerPair.first.GroundTransform.position = _startingPosition + _extensionDistance * _extensionCount * _extensionDirection;
            _extensionCount++;
        }
    }
}