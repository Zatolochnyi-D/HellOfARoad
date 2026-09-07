using System.Threading;
using DenZ.DevelopmentTools.Utilities;
using UnityEngine;
using Zenject;

namespace HoaR.Turret.Shooting
{
    public class BulletVisibilityHandler : IInitializable
    {
        private readonly GameObject _bullet;


        public BulletVisibilityHandler(GameObject bullet)
        {
            _bullet = bullet;
        }

        public void Show()
        {
            _bullet.SetActive(true);
        }

        public void Hide()
        {
            _bullet.SetActive(false);
        }

        public void Initialize()
        {
            Hide();
        }
    }
}