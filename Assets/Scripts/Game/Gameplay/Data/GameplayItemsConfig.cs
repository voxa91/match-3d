using System.Collections.Generic;
using Game.Gameplay.Item;
using MVC.Model;
using UnityEngine;

namespace Game.Gameplay.Data
{
    [CreateAssetMenu(fileName = "GameplayItemsConfig", menuName = "Game/Config/GameplayItemsConfig")]
    public class GameplayItemsConfig : ScriptableObject, IModel
    {
        [SerializeField] private List<ItemView> _itemPrefabs;
        
        public IReadOnlyList<ItemView> ItemPrefabs => _itemPrefabs;
    }
}