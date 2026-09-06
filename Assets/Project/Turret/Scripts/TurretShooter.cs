using UnityEngine;

namespace HoaR.Turret
{
    public class TurretShooter
    {
        public TurretShooter(IPointerDownUpProvider pointerDownUpProvider)
        {
            pointerDownUpProvider.OnDown += HandlePointerDown;
            pointerDownUpProvider.OnUp += HandlePointerUp;
        }

        private void HandlePointerDown()
        {
            Debug.Log("Down");
        }
        
        private void HandlePointerUp()
        {
            Debug.Log("Up");
        }
    }
}