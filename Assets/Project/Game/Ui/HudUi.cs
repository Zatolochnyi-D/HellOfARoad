using HoaR.Game.GameStateManagement;
using UnityEngine;
using Zenject;

namespace HoaR.Game.Ui
{
    public class HudUi : MonoBehaviour
    {
        [Inject] private readonly IGameStateManager<GameState> _stateManager;

        void Start()
        {
            gameObject.SetActive(false);
            _stateManager.OnStateChanged += state => { if (state == GameState.Playing) gameObject.SetActive(true); }; 
        }
    }
}