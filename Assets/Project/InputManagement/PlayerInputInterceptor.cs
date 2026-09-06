using System;
using HoaR.Turret;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HoaR.InputManagement
{
    public class PlayerInputInterceptor : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IHorizontalSwipeProvider, IPointerDownUpProvider
    {
        public event Action<float> OnHorizontalSwipe;
        public event Action OnDown;
        public event Action OnUp;

        public void OnDrag(PointerEventData eventData)
        {
            var relativeDelta = eventData.delta.x / Screen.width;
            OnHorizontalSwipe?.Invoke(relativeDelta);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnUp?.Invoke();
        }
    }
}