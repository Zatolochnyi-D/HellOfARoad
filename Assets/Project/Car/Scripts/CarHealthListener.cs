using System;
using HoaR.HealthSystem.HealthComponent;
using Zenject;

namespace HoaR.Car
{
    public class CarHealthListener : IDisposable
    {
        private readonly Health _health;
        private readonly SignalBus _signalBus;

        public CarHealthListener(Health health, SignalBus signalBus)
        {
            _health = health;
            _signalBus = signalBus;

            _health.OnHealthDepleted += FirePlayerDeadSignal;
        }

        private void FirePlayerDeadSignal()
        {
            _signalBus.TryFire<PlayerDeadSignal>();
        }

        public void Dispose()
        {
            _health.OnHealthDepleted += FirePlayerDeadSignal;
        }
    }
}