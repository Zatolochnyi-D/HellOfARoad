using HoaR.Game.GameStateManagement;
using HoaR.Game.GoalChecking;
using HoaR.Ground;
using HoaR.InputManagement;
using HoaR.Turret;
using UnityEngine;
using Zenject;

namespace HoaR.Game
{
    public class GameSceneInstaller : MonoInstaller
    {
        [Header("Goal Checker")]
        [SerializeField] private Transform _originPosition;
        [SerializeField] private Transform _destinationPosition;
        [SerializeField] private Transform _trackedPosition;

        [Header("Ground Extender")]
        [SerializeField] private GroundTriggerEnterInterceptor _firstGroundTrigger;
        [SerializeField] private GroundTriggerEnterInterceptor _secongGroundTrigger;

        public override void InstallBindings()
        {
            Container.Bind<IGameStateManager<GameState>>().To<GameStateManager>().AsSingle();

            Container.Bind<PlayerInputInterceptor>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IHorizontalSwipeProvider>().To<PlayerInputInterceptor>().FromResolve();
            Container.Bind<IPointerDownUpProvider>().To<PlayerInputInterceptor>().FromResolve();

            Container.Bind<GoalChecker>().FromSubContainerResolve().ByMethod(BindGoalChecker).AsSingle().NonLazy();
            Container.Bind<ITickable>().To<GoalChecker>().FromResolve();
            
            Container.Bind<GroundExtender>().FromSubContainerResolve().ByMethod(BindGroundExtender).AsSingle().NonLazy();
        }

        private void BindGoalChecker(DiContainer subContainer)
        {
            subContainer.BindInstance<OriginPosition>(new(_originPosition));
            subContainer.BindInstance<DestinationPosition>(new(_destinationPosition));
            subContainer.BindInstance<TrackedPosition>(new(_trackedPosition));
            subContainer.Bind<GoalChecker>().AsSingle();
        }

        private void BindGroundExtender(DiContainer subContainer)
        {
            subContainer.BindInstance((_firstGroundTrigger, _secongGroundTrigger));
            subContainer.Bind<GroundExtender>().AsSingle();
        }
    }
}