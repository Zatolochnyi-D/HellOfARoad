using System;
using HoaR.Turret;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HoaR.InputManagement
{
    public class PlayerInputInterceptor : MonoBehaviour, IDragHandler, IHorizontalSwipeProvider
    {
        public event Action<float> OnHorizontalSwipe;

        public void OnDrag(PointerEventData eventData)
        {
            var relativeDelta = eventData.delta.x / Screen.width;
            OnHorizontalSwipe?.Invoke(relativeDelta);
        }
    }
}