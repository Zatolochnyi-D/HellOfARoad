using UnityEngine;

[CreateAssetMenu(fileName = nameof(CarSettings), menuName = "Project/Car/" + nameof(CarSettings))]
public class CarSettings : ScriptableObject
{
    [SerializeField] private Vector3 _generalMovementVector = Vector3.forward;
    [SerializeField] private float _movementSpeed = 5f;

    public Vector3 GeneralMovementVector => _generalMovementVector;
    public float MovementSpeed => _movementSpeed;
}