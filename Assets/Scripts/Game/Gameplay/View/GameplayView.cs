using MVC.View;
using UnityEngine;

namespace Game.Gameplay.View
{
    public class GameplayView : BaseView
    {
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private GameplayInputHandler _gameplayInputHandler;
        [SerializeField] private Transform _matchPoint;
        
        public Transform ItemsContainer => _itemsContainer;
        public GameplayInputHandler GameplayInputHandler => _gameplayInputHandler;
        public Transform MatchPoint => _matchPoint;
    }
}