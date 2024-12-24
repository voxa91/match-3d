using Services.EventBusSystem;

namespace Game.States
{
    public interface ILobbyStateHandler : IEvent
    {
        void HandleGameplayTransition();
    }
}