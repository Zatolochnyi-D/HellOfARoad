using System;

namespace HoaR.Turret
{
    public interface IHorizontalSwipeProvider
    {
        public event Action<float> OnHorizontalSwipe;
    }
}