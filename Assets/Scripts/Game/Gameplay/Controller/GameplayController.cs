using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Item;
using Game.Gameplay.Popups;
using MVC.Controller;
using Services.Timer;

namespace Game.Gameplay.Controller
{
    public class GameplayController : BaseController
    {
        private GameplayViewController _gameplayViewController;
        private GameplayUIViewController _gameplayUIViewController;
        private ConfigsProvider _configsProvider;
        
        private Timer _timer;
        private List<ItemView> _itemViews;
        
        public async UniTask Initialize()
        {
            _configsProvider = AnyTypeResolver.Resolve<ConfigsProvider>();
            _gameplayViewController = Create<GameplayViewController>();
            _gameplayUIViewController = Create<GameplayUIViewController>();
            
            _gameplayUIViewController.Initialize();
            await _gameplayViewController.InitializeAsync();
            _gameplayViewController.OnLevelCompleted += OnLevelCompleted;
            _gameplayUIViewController.OnGamePause += OnGamePause;
            _gameplayUIViewController.OnGameResume += OnGameResumed;
            _gameplayUIViewController.OnStartLevel += LoadCurrentLevel;
            LoadCurrentLevel();
        }

        public override void Dispose()
        {
            _gameplayUIViewController.OnGamePause -= OnGamePause;
            _gameplayUIViewController.OnGameResume -= OnGameResumed;
            _gameplayUIViewController.OnStartLevel -= LoadCurrentLevel;
            _gameplayViewController.OnLevelCompleted -= OnLevelCompleted;
            base.Dispose();
        }

        private void OnGamePause()
        {
            _timer.Pause();
            _gameplayViewController.Pause();
        }

        private void OnGameResumed()
        {
            _timer.Resume();
            _gameplayViewController.Resume();
        }

        private void LoadCurrentLevel()
        {
            var levelConfig = _configsProvider.GetCurrentLevelConfig();
            _gameplayViewController.CreateLevelItems(levelConfig);
            CreateTimer(levelConfig.Time);
        }

        private void CreateTimer(float duration)
        {
            var timerService = ServiceLocator.Resolve<ITimerService>();
            _timer = timerService.StartCountdownTimer(duration, OnTimerComplete, OnTimerUpdate);
        }

        private void OnLevelCompleted()
        {
            AnyTypeResolver.Resolve<UserStateManager.UserStateManager>().IncreaseLevel();
            _timer.Stop();
            _gameplayUIViewController.CreateGameOverPopup(GameResult.Win);
        }
        
        private void OnTimerComplete()
        {
            _gameplayUIViewController.CreateGameOverPopup(GameResult.Lose);
        }

        private void OnTimerUpdate(float time)
        {
            _gameplayUIViewController.OnUpdateTime(time);
        }
    }
}