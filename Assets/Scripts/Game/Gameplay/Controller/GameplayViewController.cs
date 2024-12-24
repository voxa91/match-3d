using System;
using System.Collections.Generic;
using Game.Gameplay.Data;
using Game.Gameplay.Item;
using Game.Gameplay.View;
using MVC.Controller;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Gameplay.Controller
{
    public class GameplayViewController : BaseViewController<GameplayView>
    {
        public override string AssetName => "GameplayView";
        
        private ItemDragController _itemDragController;
        private ConfigsProvider _configsProvider;

        private LevelItemsFactory _levelItemsFactory;
        private MatchController _matchController;

        private List<ItemView> _itemViews;
        
        public event Action OnLevelCompleted;
        
        protected override void OnViewLoaded()
        {
            _itemDragController = new ItemDragController(Camera.main, View.GameplayInputHandler);
            _matchController = new MatchController(View.MatchPoint);
            _matchController.OnItemsMatched += OnItemsMatched;
            _configsProvider = AnyTypeResolver.Resolve<ConfigsProvider>();
            _levelItemsFactory = new LevelItemsFactory(_configsProvider.GameplayItemsConfig.ItemPrefabs);
        }

        public override void Dispose()
        {
            base.Dispose();
            _itemDragController.Dispose();
            _matchController.OnItemsMatched -= OnItemsMatched;
        }

        public void Pause()
        {
            View.GameplayInputHandler.enabled = false;
        }

        public void Resume()
        {
            View.GameplayInputHandler.enabled = true;
        }

        public void CreateLevelItems(LevelConfig levelConfig)
        {
            _itemViews = _levelItemsFactory.CreateItemsForLevel(View.ItemsContainer, levelConfig.UniqueItems,
                levelConfig.ItemPairs);
            foreach (var itemView in _itemViews)
            {
                itemView.OnDragEnd += OnItemDragEnd;
            }
        }

        private void OnItemDragEnd(ItemView itemView)
        {
            _matchController.TryAddMatchingItem(itemView);
        }

        private void OnItemsMatched(ItemView itemView1, ItemView itemView2)
        {
            _itemViews.Remove(itemView1);
            _itemViews.Remove(itemView2);
            Object.Destroy(itemView1.gameObject);
            Object.Destroy(itemView2.gameObject);
            CheckOnLevelCompleted();
        }

        private void CheckOnLevelCompleted()
        {
            if (_itemViews.Count == 0)
            {
                OnLevelCompleted?.Invoke();
            }
        }
    }
}