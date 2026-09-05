using UnityEngine;
using Zenject;

public class CarMover : ITickable
{
    private readonly Transform _carTransform;
    private readonly CarSettings _carSettings;

    private bool _isActive = false;

    public CarMover(Transform carTransform, CarSettings carSettings)
    {
        _carTransform = carTransform;
        _carSettings = carSettings;
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
        _carTransform.position += Time.deltaTime * _carSettings.MovementSpeed * _carSettings.GeneralMovementVector;
    }
}