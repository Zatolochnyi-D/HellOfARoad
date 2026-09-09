using HoaR.Enemies;
using HoaR.Game.GameStateManagement;
using HoaR.Game.GoalChecking;
using HoaR.Game.LevelManagement;
using HoaR.Ground;
using HoaR.InputManagement;
using HoaR.LevelManagement;
using HoaR.Turret;
using UnityEngine;
using Zenject;

namespace HoaR.Game
{
    public class GameSceneInstaller : MonoInstaller
    {
        [Header("Level")]
        [SerializeField] private LevelSettings _levelSettings;
        [SerializeField] private Transform _levelOriginPosition;

        [Header("Goal Checker")]
        [SerializeField] private Transform _destinationPosition;
        [SerializeField] private Transform _carTransform;

        [Header("Ground Extender")]
        [SerializeField] private GroundTriggerEnterInterceptor _firstGroundTrigger;
        [SerializeField] private GroundTriggerEnterInterceptor _secongGroundTrigger;

        public override void InstallBindings()
        {
            Container.BindInstance(_levelSettings);

            Container.BindInstance<LevelOrigin>(new(_levelOriginPosition));

            Container.BindInstance<CarTransform>(new(_carTransform));
            Container.Bind<IAngerTrackable>().To<CarTransform>().FromResolve();

            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();

            Container.Bind<IGameStateManager<GameState>>().To<GameStateManager>().AsSingle();

            Container.Bind<PlayerInputInterceptor>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IHorizontalSwipeProvider>().To<PlayerInputInterceptor>().FromResolve();
            Container.Bind<IPointerDownUpProvider>().To<PlayerInputInterceptor>().FromResolve();

            Container.Bind<GoalChecker>().FromSubContainerResolve().ByMethod(BindGoalChecker).AsSingle().NonLazy();
            Container.Bind<ITickable>().To<GoalChecker>().FromResolve();

            Container.Bind<GroundExtender>().FromSubContainerResolve().ByMethod(BindGroundExtender).AsSingle().NonLazy();

            Container.Bind<EnemySpawner>().FromSubContainerResolve().ByMethod(BindEnemySpawner).AsSingle().NonLazy();
        }

        private void BindGoalChecker(DiContainer subContainer)
        {
            subContainer.BindInstance<DestinationPosition>(new(_destinationPosition));
            subContainer.BindInstance<TrackedPosition>(new(_carTransform));
            subContainer.Bind<GoalChecker>().AsSingle();
        }

        private void BindGroundExtender(DiContainer subContainer)
        {
            subContainer.BindInstance((_firstGroundTrigger, _secongGroundTrigger));
            subContainer.Bind<GroundExtender>().AsSingle();
        }

        private void BindEnemySpawner(DiContainer subContainer)
        {
            subContainer.BindIFactory<GameObject, Vector3, float, Enemy>().FromFactory<EnemyFactory>();
            subContainer.Bind<EnemySpawner>().AsSingle();
        }
    }
}