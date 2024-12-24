using Services.EventBusSystem;

namespace Game.States
{
    public interface IGameplayStateHandler : IEvent
    {
        void HandleLobbyTransition();
    }
}