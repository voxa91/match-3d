using UniState;

namespace Services.ServiceResolver
{
    public interface IServiceLocator
    {
        void InitializeServices();
        void Inject<T>(T service) where T : class, IService;
        T Resolve<T>() where T : class, IService;
    }
}