using HoaR.GoalChecking;
using HoaR.Utilities;
using UnityEngine;
using Zenject;

namespace HoaR.Game.Ui
{
    public class ProgressBarUi : MonoBehaviour
    {
        [SerializeField] private ProgressBarMask _bar;

        [Inject] private readonly GoalChecker _goalChecker;

        void Awake()
        {
            _bar.SetFill(0f);
        }

        void Start()
        {
            _goalChecker.OnDistanceChanged += HandleDistanceChange;
        }

        void OnDestroy()
        {
            _goalChecker.OnDistanceChanged -= HandleDistanceChange;
        }

        private void HandleDistanceChange(float normalizedDistance)
        {
            _bar.SetFill(normalizedDistance);
        }
    }
}