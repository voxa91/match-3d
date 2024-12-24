using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Services.ServiceResolver
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, IService> _services;
        private bool _servicesInitialized;
        
        public ServiceLocator()
        {
            _services = new Dictionary<Type, IService>();
        }
        
        public void InitializeServices()
        {
            if (_servicesInitialized) return;

            _servicesInitialized = true;

            foreach (IService service in _services.Values)
            {
                service.Initialize(this);
            }
        }

        #region Get / Add Services
        
        public void Inject<T>(T injectObject) where T : class, IService
        {
            Type injectType = typeof(T);
            if (!_services.ContainsKey(injectType))
            {
                _services.Add(injectType, injectObject);

                if (_servicesInitialized && injectObject is IService service)
                {
                    service.Initialize(this);
                }
            }
            else
            {
                Debug.LogError($"[ServiceLocator] service {typeof(T).FullName} already exist");
            }
        }
        
        public T Resolve<T>() where T : class, IService
        {
            Type type = typeof(T);
            foreach (var serviceType in _services.Keys)
            {
                if (serviceType == type || serviceType.IsSubclassOf(type) || serviceType.IsAssignableTo(type))
                {
                    return _services[serviceType] as T;
                }
            }

            foreach (var serviceType in _services.Keys)
            {
                Debug.Log($"[ServiceLocator] service {serviceType.FullName} {type.FullName}  {serviceType.IsSubclassOf(type)}  {serviceType.IsAssignableTo(type)}");
            }

            Debug.LogError($"[ServiceLocator] service {type.FullName} doesn't exist");
            return null;
        }

        #endregion
        
        #region Dispose Services
        
        public void DisposeService<T>() where T : class, IService
        {
            T service = Resolve<T>();
            if (service == null) return;
            
            service.Dispose();
            
            _services.Remove(typeof(T));
        }
        
        public void DisposeAllServices()
        {
            if (_services != null)
            {
                foreach (var service in _services.Values)
                {
                    service.Dispose();
                }
                _services.Clear();
            }
        }

        #endregion
    }
}