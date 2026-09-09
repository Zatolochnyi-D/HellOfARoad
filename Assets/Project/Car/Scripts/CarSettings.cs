using HoaR.HealthSystem.HealthComponent;
using UnityEngine;

namespace HoaR.Car
{
    [CreateAssetMenu(fileName = nameof(CarSettings), menuName = "Project/Car/" + nameof(CarSettings))]
    public class CarSettings : ScriptableObject, IHealthSettingsProvider
    {
        [SerializeField] private Vector3 _generalMovementVector = Vector3.forward;
        [SerializeField] private float _movementSpeed = 5f;
        [SerializeField] private int _maxHealthPoints = 100;

        public Vector3 GeneralMovementVector => _generalMovementVector;
        public float MovementSpeed => _movementSpeed;
        public int MaxHealthPoints => _maxHealthPoints;

    }
}