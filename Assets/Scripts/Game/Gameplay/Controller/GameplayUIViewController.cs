using System;
using Game.Gameplay.Popups;
using Game.Gameplay.View;
using MVC.Controller;

namespace Game.Gameplay.Controller
{
    public class GameplayUIViewController : BaseViewController<GameplayUIView>
    {
        public override string AssetName => "GameplayUIView";

        private PausePopupController _pausePopupController;
        private GameOverPopupController _gameOverPopupController;
        
        public event Action OnGamePause;
        public event Action OnGameResume;
        public event Action OnStartLevel;
        
        protected override void OnViewLoaded()
        {
            View.Initialize(OnPauseButtonClick);
        }

        private void OnPauseButtonClick()
        {
            CreatePausePopup();
            OnGamePause?.Invoke();
        }

        private void CreatePausePopup()
        {
            _pausePopupController = Create<PausePopupController>();
            _pausePopupController.OnContinueEvent += OnPauseConfirmClick;
            _pausePopupController.Initialize();
        }

        private void OnPauseConfirmClick()
        {
            _pausePopupController.OnContinueEvent -= OnPauseConfirmClick;
            _pausePopupController.Dispose();
            _pausePopupController = null;
            OnGameResume?.Invoke();
        }

        public void CreateGameOverPopup(GameResult result)
        {
            _gameOverPopupController = Create(new GameOverPopupController(new GameOverData(result)));
            _gameOverPopupController.OnPlayEvent += OnGameOverPlayClick;
            _gameOverPopupController.Initialize();
        }

        private void OnGameOverPlayClick()
        {
            _gameOverPopupController.OnPlayEvent -= OnGameOverPlayClick;
            _gameOverPopupController.Dispose();
            _gameOverPopupController = null;
            OnStartLevel?.Invoke();
        }

        public void OnUpdateTime(float time)
        {
            View.SetTimerText(TimeToString(time));
        }

        public string TimeToString(float time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
    }
}