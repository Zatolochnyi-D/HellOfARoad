using DenZ.DevelopmentTools.Utilities;
using HoaR.HealthSystem;

namespace HoaR.Enemies
{
    public class Enemy
    {
        public Enemy(Health health)
        {
            _ = Timers.InvokeIndefinitely(() => health.ReceiveAbsoluteDamage(10), 1f);
        }
    }
}