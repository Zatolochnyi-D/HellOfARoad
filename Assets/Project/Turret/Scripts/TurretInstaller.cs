using UnityEngine;
using Zenject;

namespace HoaR.Turret
{
    public class TurretInstaller : MonoInstaller
    {
        [SerializeField] private TurretMoverSettings _turretMoverSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(transform);
            Container.BindInstance(_turretMoverSettings);

            Container.Bind<TurretShooter>().AsSingle().NonLazy();
            Container.Bind<TurretMover>().AsSingle().NonLazy();
        }
    }
}