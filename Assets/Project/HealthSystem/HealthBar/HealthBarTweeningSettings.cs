using UnityEngine;

namespace HoaR.HealthSystem
{
    [CreateAssetMenu(fileName = nameof(HealthBarTweeningSettings), menuName = "Project/Health System/" + nameof(HealthBarTweeningSettings))]
    public class HealthBarTweeningSettings : ScriptableObject
    {
        [SerializeField] private float _timeForOneTween = 0.25f;

        public float TimeForOneTween => _timeForOneTween;
    }
}