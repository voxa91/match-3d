using System;
using Game.Gameplay.Item;
using Game.Gameplay.View;
using UnityEngine;

namespace Game.Gameplay.Controller
{
    public class ItemDragController : IDisposable
    {
        private Camera _camera;
        private GameplayInputHandler _inputHandler;
        
        private ItemView _item;
        private bool _isDragging;
        private Vector3 _originalOffset;
        
        private int _layerMask;

        private const float DRAG_HEIGHT = 2f;
        private const string ITEM_LAYER = "Item";
        
        public ItemDragController(Camera camera, GameplayInputHandler inputHandler)
        {
            _layerMask = LayerMask.GetMask(ITEM_LAYER);
            
            _camera = camera;
            _inputHandler = inputHandler;
            _inputHandler.OnBeginDrag += OnBeginDrag;
            _inputHandler.OnEndDrag += OnEndDrag;
            _inputHandler.OnDrag += OnDrag;
        }

        public void Dispose()
        {
            _inputHandler.OnBeginDrag -= OnBeginDrag;
            _inputHandler.OnEndDrag -= OnEndDrag;
            _inputHandler.OnDrag -= OnDrag;
        }

        private void OnBeginDrag()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100, _layerMask) && 
                    hit.collider.gameObject.TryGetComponent<ItemView>(out var itemView))
                {
                    _item = itemView;
                    _originalOffset = _item.transform.position - hit.point;
                    _isDragging = true;
                    _item.SetDraggable(true);
                }
            }
        }
        
        private void OnEndDrag()
        {
            if(_isDragging)
            {
                _isDragging = false;
                _item.SetDraggable(false);
                _item.FireOnDragEnd();
            }
        }
        
        private void OnDrag()
        {
            if (_isDragging)
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 targetPosition = hit.point + _originalOffset;
                    targetPosition.y = DRAG_HEIGHT;
                    _item.MoveToPosition(targetPosition);
                }
            }
        }
    }
}