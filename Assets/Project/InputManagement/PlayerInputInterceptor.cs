using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HoaR.InputManagement
{
    public class PlayerInputInterceptor : MonoBehaviour, IDragHandler
    {
        public event Action<float> OnHorizontalSwipe;

        public void OnDrag(PointerEventData eventData)
        {
            var relativeDelta = eventData.delta.x / Screen.width;
            OnHorizontalSwipe?.Invoke(relativeDelta);
        }
    }
}