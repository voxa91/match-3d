using System;
using UnityEngine;

namespace Game.Gameplay.View
{
    public class GameplayInputHandler : MonoBehaviour
    {
        public event Action OnBeginDrag;
        public event Action OnDrag;
        public event Action OnEndDrag;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnBeginDrag?.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnEndDrag?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            OnDrag?.Invoke();
        }
    }
}