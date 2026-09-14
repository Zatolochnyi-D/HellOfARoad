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
        private float _fillAmount;

        public float FillAmount => _fillAmount;

        void Awake()
        {
            _fillFunc = _fillDirection switch
            {
                FillDirection.Left => fill => _mask.padding = _mask.padding.With(z: _self.rect.size.x * (1f - fill)),
                FillDirection.Up => fill => _mask.padding = _mask.padding.With(w: _self.rect.size.y * (1f - fill)),
                _ => throw FastExeptions.NonExistentEnumValue<FillDirection>(),
            };

            _fillAmount = 0f;
            _fillFunc(_fillAmount);
        }

        public void SetFill(float fill)
        {
            _fillAmount = Mathf.Clamp01(fill);
            _fillFunc(_fillAmount);
        }
    }
}