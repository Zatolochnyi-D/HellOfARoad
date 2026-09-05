using HoaR.Game.GameStateManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class StartingScreenUi : MonoBehaviour, IPointerClickHandler
{
    [Inject] private IGameStateManager<GameState> _stateManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        _stateManager.ChangeState(GameState.Playing);
        gameObject.SetActive(false);
    }
}