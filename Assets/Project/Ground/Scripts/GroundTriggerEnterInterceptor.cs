using System;
using UnityEngine;

namespace HoaR.Ground
{
    public class GroundTriggerEnterInterceptor : MonoBehaviour
    {
        public event Action OnCarEnteredTrigger;

        public Transform GroundTransform => transform.parent;

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Ding");
            OnCarEnteredTrigger?.Invoke();
        }
    }
}