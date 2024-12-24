using MVC.View;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Gameplay.Popups
{
    public class GameOverPopupView : BaseView
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _playButtonText;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;

        private const string YOU_WIN = "YOU WIN";
        private const string GAME_OVER = "GAME OVER";
        private const string PLAY_NEXT = "Play Next";
        private const string RESTART = "Restart Game";
        
        public void Initialize(GameResult gameResult, UnityAction onPlayClick, UnityAction onExitClick)
        {
            _titleText.text = gameResult == GameResult.Win ? YOU_WIN : GAME_OVER;
            _playButtonText.text = gameResult == GameResult.Win ? PLAY_NEXT : RESTART;
            _playButton.onClick.AddListener(onPlayClick);
            _exitButton.onClick.AddListener(onExitClick);
        }
    }
}