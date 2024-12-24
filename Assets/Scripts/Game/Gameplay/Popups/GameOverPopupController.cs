using System;
using Game.States;
using MVC.Controller;
using Services.EventBusSystem;

namespace Game.Gameplay.Popups
{
    public class GameOverPopupController : BaseViewController<GameOverPopupView, GameOverData>
    {
        public override string AssetName => "GameOverPopup";
        
        public event Action OnPlayEvent;
        
        public GameOverPopupController(GameOverData model) : base(model)
        {
        }
        
        protected override void OnViewLoaded()
        {
            View.Initialize(Model.Result, OnPlayNextLevelClick, OnExitClick);
        }

        private void OnPlayNextLevelClick()
        {
            OnPlayEvent?.Invoke();
        }

        private void OnExitClick()
        {
            ServiceLocator.Resolve<EventBus>().RaiseEvent<IGameplayStateHandler>(handler => handler.HandleLobbyTransition());
        }
    }
}