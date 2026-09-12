using HoaR.Game.GameStateManagement;
using UnityEngine;
using Zenject;

namespace HoaR.Game.Ui
{
    public class ModifyVisibilityOnStateChange : MonoBehaviour
    {
        [Inject] private readonly IGameStateManager<GameState> _stateManager;

        void Start()
        {
            gameObject.SetActive(false);
            _stateManager.OnStateChanged += HandleStateChange;
        }

        void OnDestroy()
        {
            _stateManager.OnStateChanged -= HandleStateChange;
        }

        private void HandleStateChange(GameState newState)
        {
            if (newState == GameState.Playing) 
                gameObject.SetActive(true);
        }
    }
}