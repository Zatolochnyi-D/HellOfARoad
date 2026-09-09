using System;
using HoaR.Turret.Shooting;
using UnityEngine;
using Zenject;

namespace HoaR.Game
{
    public class KillCounter : IDisposable
    {
        public event Action OnKillRegistered;

        private readonly SignalBus _signalBus;

        private int _killCount = 0;

        public int KillCount => _killCount;

        public KillCounter(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _signalBus.Subscribe<KillSignal>(HandleKill);
        }

        private void HandleKill()
        {
            _killCount++;
            OnKillRegistered?.Invoke();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<KillSignal>(HandleKill);
        }
    }
}