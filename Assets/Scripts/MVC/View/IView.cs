using System;
using UnityEngine;

namespace MVC.View
{
    public interface IView : IDisposable
    {
        event Action<IView> OnDisposed;
        
        Transform Transform { get; }

        bool Enabled { get; set; }
    }
}