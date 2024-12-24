using MVC.View;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Lobby
{
    public class LobbyView : BaseView
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private TMP_Text _playText;

        private const string PLAY_BUTTON_TEXT = "Play {0}";
        
        public void Initialize(int level, UnityAction onClick)
        {
            _playButton.onClick.AddListener(onClick);
            _playText.text = string.Format(PLAY_BUTTON_TEXT, level);
        }
    }
}