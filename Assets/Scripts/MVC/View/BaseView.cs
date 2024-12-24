using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MVC.View
{
    public abstract class BaseView : MonoBehaviour, IView
    {
		private bool _disposed;

		public bool IsDisposed => _disposed;

		protected void ThrowIfDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException(GetType().Name);
			}
		}

		protected virtual void OnDispose()
		{
		}
		
		public event Action<IView> OnDisposed;

		public Transform Transform => transform;

		public bool Enabled
		{
			get
			{
				return gameObject.activeSelf;
			}
			set
			{
				ThrowIfDisposed();
				gameObject.SetActive(value);
			}
		}

		public void Dispose()
		{
			if (!_disposed)
			{
				_disposed = true;

				try
				{
					OnDispose();
				}
				finally
				{
					OnDisposed?.Invoke(this);
				}
			}
		}
    }
}