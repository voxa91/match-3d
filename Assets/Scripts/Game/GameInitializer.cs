using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Controller;
using MVC;
using Services.EventBusSystem;
using Services.ResourceProvider;
using Services.ServiceResolver;
using Services.StateMachine;
using Services.Timer;
using UniState;
using UnityEngine;

namespace Game
{
    public class GameInitializer : MonoBehaviour
    {
        private CancellationTokenSource _ctx;
        private IServiceLocator _serviceLocator;
        private IAnyTypeResolver _anyTypeResolver;

        private void Start()
        {
            InitializeServices();
            InitializeStateMachine();
            InitializeManagers();
            Run().Forget();
        }

        private void InitializeServices()
        {
            _serviceLocator = new ServiceLocator();
            var anyTypeResolver = new AnyTypeResolver();
            _anyTypeResolver = anyTypeResolver;
            _serviceLocator.Inject(anyTypeResolver);
            _serviceLocator.Inject(new EventBus());
            _serviceLocator.Inject(new TimerService());
            _serviceLocator.Inject(new AddressablesResourceProvider());
            _serviceLocator.Inject(new ControllerFactory());
            _serviceLocator.InitializeServices();
        }

        private void InitializeStateMachine()
        {
            LobbyState lobbyState = new LobbyState(_serviceLocator);
            GameplayState gameplayState = new GameplayState(_serviceLocator);
            
            _anyTypeResolver.Add(new MyStateMachine());
            _anyTypeResolver.Add(lobbyState);
            _anyTypeResolver.Add(gameplayState);
        }

        private void InitializeManagers()
        {
            var controllerFactory = _serviceLocator.Resolve<IControllerFactory>();
            var userStateManager = controllerFactory.Create<UserStateManager.UserStateManager>();
            userStateManager.Initialize();
            _anyTypeResolver.Add(userStateManager);

            var configsProvider = controllerFactory.Create<ConfigsProvider>();
            configsProvider.Initialize();
            _anyTypeResolver.Add(configsProvider);
        }

        public async UniTaskVoid Run()
        {
            _ctx = new CancellationTokenSource();
            var stateMachine =  StateMachineHelper.CreateStateMachine<StateMachine>(_anyTypeResolver);
            await stateMachine.Execute<LobbyState>(_ctx.Token);
        }
    }
}