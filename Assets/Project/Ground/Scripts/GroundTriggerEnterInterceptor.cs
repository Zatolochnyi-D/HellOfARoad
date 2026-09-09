using System;
using UnityEngine;

namespace HoaR.Ground
{
    public class GroundTriggerEnterInterceptor : MonoBehaviour
    {
        [SerializeField] private LayerMask _triggerOn;

        public event Action OnCarEnteredTrigger;

        public Transform GroundTransform => transform.parent;

        void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & _triggerOn.value) == 0)
                return;
            OnCarEnteredTrigger?.Invoke();
        }
    }
}