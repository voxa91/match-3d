namespace Services.ServiceResolver
{
    public abstract class BaseService : IService
    {
        protected IServiceLocator ServiceLocator { get; private set; }
        public virtual void Initialize(IServiceLocator serviceLocator)
        {
            ServiceLocator = serviceLocator;
        }
        
        public virtual void Dispose()
        {
        }
    }
}