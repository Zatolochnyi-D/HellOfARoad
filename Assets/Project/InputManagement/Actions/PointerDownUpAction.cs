using System;
using DenZ.DevelopmentTools.InputSystem;
using UnityEngine.InputSystem;

namespace HoaR.InputManagement
{
    public class PointerDownUpAction : IStartCancelAction
    {
        public event Action OnStarted;
        public event Action OnCanceled;

        public PointerDownUpAction(InputAction pointerDownUpAction)
        {
            pointerDownUpAction.performed += _ => OnStarted?.Invoke();
            pointerDownUpAction.canceled += _ => OnCanceled?.Invoke();
        }
    }
}