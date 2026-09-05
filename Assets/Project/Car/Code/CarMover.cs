using UnityEngine;
using Zenject;

public class CarMover : ITickable
{
    private static readonly Vector3 GENERAL_MOVEMENT_DIRECTION = new(0f, 0f, 1f);
    private const float MOVEMENT_SPEED = 5f;

    private readonly Transform _carTransform;

    private bool _isActive = false;

    public CarMover(Transform carTransform)
    {
        _carTransform = carTransform;
    }

    public void Enable()
    {
        _isActive = true;
    }

    public void Disable()
    {
        _isActive = false;
    }

    public void Tick()
    {
        if (!_isActive)
            return;
        _carTransform.position += Time.deltaTime * MOVEMENT_SPEED * GENERAL_MOVEMENT_DIRECTION;
    }
}