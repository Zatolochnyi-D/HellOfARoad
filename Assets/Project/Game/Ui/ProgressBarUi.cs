using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HoaR.Game.Ui
{
    public class ProgressBarUi : MonoBehaviour
    {
        [SerializeField] private Image _fillableImage;

        [Inject] private readonly GoalChecker _goalChecker;

        void Awake()
        {
            _fillableImage.fillAmount = 0f;
        }

        void Start()
        {
            _goalChecker.OnDistanceChanged += HandleDistanceChange;
        }

        private void HandleDistanceChange(float normalizedDistance)
        {
            _fillableImage.fillAmount = normalizedDistance;
        }
    }
}