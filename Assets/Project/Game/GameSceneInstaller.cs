using HoaR.Game.GameStateManagement;
using HoaR.Game.GoalChecking;
using HoaR.Ground;
using UnityEngine;
using Zenject;

namespace HoaR.Game
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform _originPosition;
        [SerializeField] private Transform _destinationPosition;
        [SerializeField] private Transform _trackedPosition;
        [SerializeField] private GroundTriggerEnterInterceptor _firstGroundTrigger;
        [SerializeField] private GroundTriggerEnterInterceptor _secongGroundTrigger;

        public override void InstallBindings()
        {
            Container.BindInstance<OriginPosition>(new(_originPosition));
            Container.BindInstance<DestinationPosition>(new(_destinationPosition));
            Container.BindInstance<TrackedPosition>(new(_trackedPosition));

            Container.Bind<IGameStateManager<GameState>>().To<GameStateManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GoalChecker>().AsSingle().NonLazy();
            Container.Bind<GroundExtender>().FromSubContainerResolve().ByMethod(BindGroundExtender).AsSingle().NonLazy();
        }

        private void BindGroundExtender(DiContainer subContainer)
        {
            subContainer.BindInstance((_firstGroundTrigger, _secongGroundTrigger));
            subContainer.Bind<GroundExtender>().AsSingle();
        }
    }
}