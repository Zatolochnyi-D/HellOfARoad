using UnityEngine;
using Zenject;

namespace HoaR.Utilities
{
    public class FaceScreenPlane : MonoBehaviour
    {
        [Inject] private readonly Camera _camera;

        void Update()
        {
            transform.forward = _camera.transform.forward;
        }
    }
}