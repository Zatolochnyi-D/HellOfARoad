using System;
using HoaR.Game.GameStateManagement;
using HoaR.Turret;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace HoaR.InputManagement
{
    public class PlayerInputInterceptor : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IHorizontalSwipeProvider, IPointerDownUpProvider
    {
        public event Action<float> OnHorizontalSwipe;
        public event Action OnDown;
        public event Action OnUp;

        [Inject] private readonly IGameStateManager<GameState> _gameStateManager;

        private bool _isDown = false;

        void Awake()
        {
            _gameStateManager.OnStateChanged += HandleGameStateChange;
        }

        void OnDestroy()
        {
            _gameStateManager.OnStateChanged -= HandleGameStateChange;
        }

        private void HandleGameStateChange(GameState newState)
        {
            if (_isDown && (newState == GameState.GameWon || newState == GameState.GameLost))
            {
                OnUp?.Invoke();
                _isDown = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            var relativeDelta = eventData.delta.x / Screen.width;
            if (_isDown)
                OnHorizontalSwipe?.Invoke(relativeDelta);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDown = true;
            OnDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDown = false;
            OnUp?.Invoke();
        }
    }
}