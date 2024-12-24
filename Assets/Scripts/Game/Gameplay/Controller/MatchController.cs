using System;
using DG.Tweening;
using Game.Gameplay.Item;
using UnityEngine;

namespace Game.Gameplay.Controller
{
    public class MatchController
    {
        private readonly Transform _matchPoint;
        private readonly Vector2 _matchCenter;
        private readonly Vector3 _leftMatchingPosition, _rightMatchingPosition;
        
        private ItemView _leftItem, _rightItem;

        private const float MATCH_RADIUS = 3f;
        private const float MATCH_ITEM_HEIGHT = 0.5f;
        private const float MATCH_ITEM_OFFSET = 0.75f;
        
        private const float MOVE_TIME = 0.3f;
        private const float MATCH_DURATION = 0.5f;

        public event Action<ItemView, ItemView> OnItemsMatched;
        
        public MatchController(Transform matchPoint)
        {
            var position = matchPoint.position;
            _matchPoint = matchPoint;
            _matchCenter = new Vector2(position.x, position.z);
            _leftMatchingPosition = new Vector3(position.x - MATCH_ITEM_OFFSET, MATCH_ITEM_HEIGHT, position.z);
            _rightMatchingPosition = new Vector3(position.x + MATCH_ITEM_OFFSET, MATCH_ITEM_HEIGHT, position.z);
        }

        public void TryAddMatchingItem(ItemView itemView)
        {
            if (IsItemInMatchingZone(itemView))
            {
                if (_leftItem == null)
                {
                    MoveItemToMatchingPosition(itemView, _leftMatchingPosition, () =>
                    {
                        _leftItem = itemView;
                        TryMatchItems();
                    });
                }
                else if (_rightItem == null)
                {
                    MoveItemToMatchingPosition(itemView, _rightMatchingPosition, () =>
                    {
                        _rightItem = itemView;
                        TryMatchItems();
                    });
                }
            }
        }

        private bool IsItemInMatchingZone(ItemView itemView)
        {
            Vector2 itemPos = new Vector2(itemView.transform.position.x, itemView.transform.position.z);
            return Vector2.Distance(_matchCenter, itemPos) <= MATCH_RADIUS;
        }
        
        private void MoveItemToMatchingPosition(ItemView itemView, Vector3 position, Action onComplete)
        {
            itemView.SetMatching(true);
            itemView.transform.DOMove(position, MOVE_TIME).OnComplete(() => onComplete?.Invoke());
            itemView.transform.DORotate(Vector3.zero, MOVE_TIME);
        }

        private void TryMatchItems()
        {
            if (_leftItem != null && _rightItem != null)
            {
                if (_leftItem.TypeId == _rightItem.TypeId)
                {
                    PlayMatchingAnimation();
                }
                else
                {
                    ThrowMatchingItems();
                }
            }
        }

        private void PlayMatchingAnimation()
        {
            _leftItem.transform.DOMove(_matchPoint.position, MATCH_DURATION);
            _rightItem.transform.DOMove(_matchPoint.position, MATCH_DURATION).OnComplete(() =>
            {
                OnItemsMatched?.Invoke(_leftItem, _rightItem);
                _leftItem = null;
                _rightItem = null;
            });
        }

        private void ThrowMatchingItems()
        {
            ThrowAwayItem(_leftItem);
            ThrowAwayItem(_rightItem);
            
            _leftItem = null;
            _rightItem = null;
        }

        private void ThrowAwayItem(ItemView itemView)
        {
            itemView.SetMatching(false);
            itemView.ThrowAway();
        }
    }
}