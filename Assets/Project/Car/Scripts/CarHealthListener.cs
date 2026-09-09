using HoaR.HealthSystem.HealthComponent;
using Zenject;

namespace HoaR.Car
{
    public class CarHealthListener
    {
        private readonly Health _health;

        public CarHealthListener(Health health, SignalBus signalBus)
        {
            _health = health;

            _health.OnHealthDepleted += signalBus.TryFire<PlayerDeadSignal>;
        }
    }
}