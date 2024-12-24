using MVC.View;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Gameplay.Popups
{
    public class PausePopupView : BaseView
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;

        public void Initialize(UnityAction onContinueClick, UnityAction onExitClick)
        {
            _continueButton.onClick.AddListener(onContinueClick);
            _exitButton.onClick.AddListener(onExitClick);
        }
    }
}