using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Controller;
using Game.States;
using MVC;
using Services.EventBusSystem;
using Services.ServiceResolver;
using Services.StateMachine;
using UniState;

public class GameplayState : MyStateBase, IGameplayStateHandler
{
    private GameplayController _gameplayController;
    private bool _goToLobby = false;
    
    public GameplayState(IServiceLocator serviceLocator) : base(serviceLocator)
    {
    }
    
    public override UniTask Initialize(CancellationToken token)
    {
        _goToLobby = false;
        var controllerFactory = ServiceLocator.Resolve<IControllerFactory>();
        _gameplayController = controllerFactory.Create<GameplayController>();
        _gameplayController.Initialize().Forget();
        Disposables.Add(_gameplayController);
        ServiceLocator.Resolve<EventBus>().Subscribe(this);
        return base.Initialize(token);
    }

    public override async UniTask<StateTransitionInfo> Execute(CancellationToken token)
    {
        await UniTask.WaitUntil(() => _goToLobby, cancellationToken: token);
        return Transition.GoTo<LobbyState>();
    }

    public override UniTask Exit(CancellationToken token)
    {
        ServiceLocator.Resolve<EventBus>().Subscribe(this);
        return base.Exit(token);
    }

    public void HandleLobbyTransition()
    {
        _goToLobby = true;
    }
}
