using System;
using DenZ.DevelopmentTools;
using DenZ.DevelopmentTools.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace HoaR.Utilities
{
    public enum FillDirection
    {
        Left,
        Up,
    }

    // RectMask2D padding is represented with Vector4, where x = Left, y = Bottom, z = Right, w = Top
    public class ProgressBarMask : MonoBehaviour
    {
        [SerializeField] private RectTransform _self;
        [SerializeField] private RectMask2D _mask;
        [SerializeField] private FillDirection _fillDirection;

        private Action<float> _fillFunc;

        void Awake()
        {
            _fillFunc = _fillDirection switch
            {
                FillDirection.Left => fill => _mask.padding = _mask.padding.With(z: _self.rect.size.x * (1f - fill)),
                FillDirection.Up => fill => _mask.padding = _mask.padding.With(w: _self.rect.size.y * (1f - fill)),
                _ => throw FastExeptions.NonExistentEnumValue<FillDirection>(),
            };
        }

        public void SetFill(float fill)
        {
            _fillFunc(fill);
        }
    }
}