using HoaR.Car;
using HoaR.Game.GameStateManagement;
using HoaR.GoalChecking;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;

namespace HoaR.LevelManagement
{
    public class GameEndUi : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject _gameWinScreenRoot;
        [SerializeField] private GameObject _gameLoseScreenRoot;
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IGameStateManager<GameState> _gameStateManager;

        void Start()
        {
            gameObject.SetActive(false);
            _gameWinScreenRoot.SetActive(false);
            _gameLoseScreenRoot.SetActive(false);

            _signalBus.Subscribe<PlayerDeadSignal>(HandlePlayerDeadSignal);
            _signalBus.Subscribe<PlayerReachedDestinationSignal>(HandlePlayerReachedDestinationSignal);
        }

        void OnDestroy()
        {
            _signalBus.Unsubscribe<PlayerDeadSignal>(HandlePlayerDeadSignal);
            _signalBus.Unsubscribe<PlayerReachedDestinationSignal>(HandlePlayerReachedDestinationSignal);
        }

        private void HandlePlayerDeadSignal()
        {
            gameObject.SetActive(true);
            _gameLoseScreenRoot.SetActive(true);
            _gameStateManager.ChangeState(GameState.GameLost);
        }

        private void HandlePlayerReachedDestinationSignal()
        {
            gameObject.SetActive(true);
            _gameWinScreenRoot.SetActive(true);
            _gameStateManager.ChangeState(GameState.GameWon);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SceneManager.LoadScene(0);
        }
    }
}