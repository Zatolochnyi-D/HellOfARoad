using HoaR.InputManagement;
using UnityEngine;

namespace HoaR.Turret
{
    public class TurretShooter
    {
        public TurretShooter(InputManager inputManager)
        {
            inputManager.PointerDownUp.OnStarted += () => Debug.Log("Start shooting");
            inputManager.PointerDownUp.OnCanceled += () => Debug.Log("End shooting");
        }

    }
}