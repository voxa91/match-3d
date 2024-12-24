using Game.States;
using MVC.Controller;
using Services.EventBusSystem;

namespace Game.Lobby
{
    public class LobbyViewController : BaseViewController<LobbyView>
    {
        public override string AssetName => "LobbyView";
        
        protected override void OnViewLoaded()
        {
            var userStateManager = AnyTypeResolver.Resolve<UserStateManager.UserStateManager>();
            View.Initialize(userStateManager.UserStateData.Level, OnPlayClick);
        }

        private void OnPlayClick()
        {
            ServiceLocator.Resolve<EventBus>().RaiseEvent<ILobbyStateHandler>(handler => handler.HandleGameplayTransition());
        }
    }
}