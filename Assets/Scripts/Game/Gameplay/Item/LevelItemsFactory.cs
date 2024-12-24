using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Game.Gameplay.Item
{
    public class LevelItemsFactory
    {
        private readonly List<ItemView> _itemPrefabs;
        private Transform _itemContainer;

        private const float SPAWN_RADIUS = 3f;
        private const float MIN_SPAWN_HEIGHT = 0.5f;
        private const float MAX_SPAWN_HEIGHT = 1.5f;
        
        public LevelItemsFactory(IReadOnlyList<ItemView> itemPrefabs)
        {
            _itemPrefabs = itemPrefabs.ToList();
        }

        public List<ItemView> CreateItemsForLevel(Transform itemsContainer, int uniqueItems, int itemPairs)
        {
            _itemContainer = itemsContainer;
            var uniqueItemPrefabs = PrepareUniqueItemsForLevel(uniqueItems);
            return CreateItemPairs(uniqueItemPrefabs, itemPairs);
        }

        private List<ItemView> PrepareUniqueItemsForLevel(int uniqueItems)
        {
            uniqueItems = Mathf.Min(uniqueItems, _itemPrefabs.Count);
            ListUtils.Shuffle(_itemPrefabs);
            List<ItemView> uniqueItemPrefabs = new List<ItemView>(uniqueItems);
            for (int i = 0; i < uniqueItems; i++)
            {
                uniqueItemPrefabs.Add(_itemPrefabs[i]);
            }
            return uniqueItemPrefabs;
        }

        private List<ItemView> CreateItemPairs(List<ItemView> uniqueItemPrefabs, int itemPairs)
        {
            List<ItemView> itemViews = new List<ItemView>(itemPairs * 2);
            int uniqueItemsCount = uniqueItemPrefabs.Count;
            for (int i = 0; i < itemPairs; i++)
            {
                int uniqueItemNumber = i % uniqueItemsCount;
                ItemView itemPrefab = uniqueItemPrefabs[uniqueItemNumber];
                CreateItemPair(uniqueItemNumber, itemPrefab, ref itemViews);
                CreateItemPair(uniqueItemNumber, itemPrefab, ref itemViews);
            }
            return itemViews;
        }

        private void CreateItemPair(int uniqueItemNumber, ItemView itemPrefab, ref List<ItemView> items)
        {
            var itemView = Object.Instantiate(itemPrefab, _itemContainer);
            Vector2 spawnPosition = Random.insideUnitCircle * SPAWN_RADIUS;
            itemView.transform.localPosition = new Vector3(spawnPosition.x, Random.Range(MIN_SPAWN_HEIGHT, MAX_SPAWN_HEIGHT), spawnPosition.y);
            itemView.SetTypeId(uniqueItemNumber);
            items.Add(itemView);
        }
    }
}