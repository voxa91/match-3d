using Game.Gameplay.Controller;
using MVC.View;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Gameplay.View
{
    public class GameplayUIView : BaseView
    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private Button _pauseButton;

        public void Initialize(UnityAction onPauseClick)
        {
            _pauseButton.onClick.AddListener(onPauseClick);
        }

        public void SetTimerText(string text)
        {
            _timerText.text = text;
        }
    }
}