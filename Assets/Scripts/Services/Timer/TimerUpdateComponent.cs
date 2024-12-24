using System;
using UnityEngine;

namespace Services.Timer
{
    public class TimerUpdateComponent : MonoBehaviour
    {
        public event Action OnUpdate;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}