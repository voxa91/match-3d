using System;
using UnityEngine;

namespace Game.Gameplay.Item
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;

        public event Action<ItemView> OnDragEnd;
        
        public int TypeId { get; private set; }

        public void SetTypeId(int typeId)
        {
            TypeId = typeId;
        }

        public void SetDraggable(bool draggable)
        {
            _rigidbody.isKinematic = draggable;
        }

        public void SetMatching(bool matching)
        {
            _rigidbody.isKinematic = matching;
            _collider.enabled = !matching;
        }

        public void MoveToPosition(Vector3 position)
        {
            _rigidbody.MovePosition(position);
        }

        public void FireOnDragEnd()
        {
            OnDragEnd?.Invoke(this);
        }

        public void ThrowAway()
        {
            _rigidbody.AddForce(0, 0, 10, ForceMode.Impulse);
        }
    }
}