using System;

namespace HoaR.Turret
{
    public interface IPointerDownUpProvider
    {
        public event Action OnDown;
        public event Action OnUp;
    }
}