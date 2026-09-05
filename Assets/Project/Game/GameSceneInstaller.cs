using HoaR.Game.GameStateManagement;
using UnityEngine;
using Zenject;

namespace HoaR.Game
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform _originPosition;
        [SerializeField] private Transform _destinationPosition;
        [SerializeField] private Transform _trackedPosition;

        public override void InstallBindings()
        {
            Container.BindInstance<OriginPosition>(new(_originPosition));
            Container.BindInstance<DestinationPosition>(new(_destinationPosition));
            Container.BindInstance<TrackedPosition>(new(_trackedPosition));
            
            Container.Bind<IGameStateManager<GameState>>().To<GameStateManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GoalChecker>().AsSingle().NonLazy();
        }
    }
}