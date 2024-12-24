using System;

namespace Services.ServiceResolver
{
    public interface IService : IDisposable
    {
        public void Initialize(IServiceLocator serviceLocator);
    }
}