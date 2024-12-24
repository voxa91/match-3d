using MVC.Model;
using UnityEngine;

namespace Game.Gameplay.Data
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/Config/LevelConfig")]
    public class LevelConfig : ScriptableObject, IModel
    {
        [SerializeField] private int _time;
        [SerializeField] private int _uniqueItems;
        [SerializeField] private int _itemPairs;
        
        public int Time => _time;
        public int UniqueItems => _uniqueItems;
        public int ItemPairs => _itemPairs;
    }
}