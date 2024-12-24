using System;
using Game.States;
using MVC.Controller;
using Services.EventBusSystem;

namespace Game.Gameplay.Popups
{
    public class PausePopupController : BaseViewController<PausePopupView>
    {
        public override string AssetName => "PausePopup";

        public event Action OnContinueEvent;
        
        protected override void OnViewLoaded()
        {
            View.Initialize(OnContinueClick, OnExitClick);
        }

        private void OnContinueClick()
        {
            OnContinueEvent?.Invoke();
        }

        private void OnExitClick()
        {
            ServiceLocator.Resolve<EventBus>().RaiseEvent<IGameplayStateHandler>(handler => handler.HandleLobbyTransition());
        }
    }
}