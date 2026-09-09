using HoaR.Game;
using TMPro;
using UnityEngine;
using Zenject;

namespace HoaR.LevelManagement
{
    public class KillCounterUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;
        [Inject] private readonly KillCounter _killCounter;

        void Start()
        {
            _counterText.text = "0";

            _killCounter.OnKillRegistered += () => _counterText.text = _killCounter.KillCount.ToString();
        }
    }
}