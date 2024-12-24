using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Lobby;
using Game.States;
using MVC;
using Services.EventBusSystem;
using Services.ServiceResolver;
using Services.StateMachine;
using UniState;
using UnityEngine;

public class LobbyState : MyStateBase, ILobbyStateHandler
{
    private LobbyViewController _lobbyViewController;
    private bool _goToGameplay = false;

    public LobbyState(IServiceLocator serviceLocator) : base(serviceLocator)
    {
    }

    public override UniTask Initialize(CancellationToken token)
    {
        _goToGameplay = false;
        var controllerFactory = ServiceLocator.Resolve<IControllerFactory>();
        _lobbyViewController = controllerFactory.Create<LobbyViewController>();
        _lobbyViewController.Initialize();
        Disposables.Add(_lobbyViewController);
        ServiceLocator.Resolve<EventBus>().Subscribe(this);
        return base.Initialize(token);
    }

    public override async UniTask<StateTransitionInfo> Execute(CancellationToken token)
    {
        await UniTask.WaitUntil(() => _goToGameplay, cancellationToken: token);
        return Transition.GoTo<GameplayState>();
    }

    public override UniTask Exit(CancellationToken token)
    {
        ServiceLocator.Resolve<EventBus>().Unsubscribe(this);
        return base.Exit(token);
    }

    public void HandleGameplayTransition()
    {
        _goToGameplay = true;
    }
}
