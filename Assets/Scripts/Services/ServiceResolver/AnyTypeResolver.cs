using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Services.ServiceResolver
{
    public class AnyTypeResolver : BaseService, IAnyTypeResolver
    {
        private readonly Dictionary<Type, object> _typedObjects;
        
        public AnyTypeResolver()
        {
            _typedObjects = new Dictionary<Type, object>();
        }
        
        #region Get / Add
        
        public void Add<T>(T injectObject) where T : class
        {
            Type injectType = typeof(T);
            if (!_typedObjects.ContainsKey(injectType))
            {
                _typedObjects.Add(injectType, injectObject);
            }
            else
            {
                Debug.LogError($"[AnyTypeResolver] type {typeof(T).FullName} already exist");
            }
        }

        public T Resolve<T>() where T : class
        {
            Type type = typeof(T);
            var resolveObject = Resolve(type);
            if (resolveObject is T typedObject)
            {
                return typedObject;
            }

            return null;
        }
        
        public object Resolve(Type type)
        {
            foreach (var serviceType in _typedObjects.Keys)
            {
                if (serviceType == type || serviceType.IsSubclassOf(type) || serviceType.IsAssignableTo(type))
                {
                    return _typedObjects[serviceType];
                }
            }
            
            Debug.LogError($"[AnyTypeResolver] type {type.FullName} doesn't exist");
            return null;
        }

        #endregion
        
        public override void Dispose()
        {
            if (_typedObjects != null)
            {
                foreach (var service in _typedObjects.Values)
                {
                    if (service is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
                _typedObjects.Clear();
            }
            
        }
    }
}