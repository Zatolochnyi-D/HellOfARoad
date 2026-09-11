using HoaR.LevelManagement;
using UnityEngine;

namespace HoaR.Ground
{
    public class GroundExtender
    {
        private readonly Transform _levelOrigin;

        private readonly float _extensionDistance;

        public GroundExtender(LevelOrigin levelOrigin,
                             (GroundTriggerEnterInterceptor, GroundTriggerEnterInterceptor, GroundTriggerEnterInterceptor) groundTriggerPair)
        {
            _levelOrigin = levelOrigin.Value;

            var (first, second, third) = groundTriggerPair;
            var vectorDifference = second.GroundTransform.position - first.GroundTransform.position;
            _extensionDistance = vectorDifference.magnitude;
            
            first.OnCarEnteredTrigger += () => HandleTriggerEnter(second, first);
            second.OnCarEnteredTrigger += () => HandleTriggerEnter(third, second);
            third.OnCarEnteredTrigger += () => HandleTriggerEnter(first, third);
        }

        private void HandleTriggerEnter(GroundTriggerEnterInterceptor planeToBeMoved, GroundTriggerEnterInterceptor planeToPutAfter)
        {
            planeToBeMoved.GroundTransform.position = planeToPutAfter.GroundTransform.position + _extensionDistance * _levelOrigin.forward;
        }
    }
}