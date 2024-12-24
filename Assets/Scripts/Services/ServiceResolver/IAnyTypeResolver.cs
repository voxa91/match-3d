using UniState;

namespace Services.ServiceResolver
{
    public interface IAnyTypeResolver : ITypeResolver, IService
    {
        void Add<T>(T service) where T : class;
        T Resolve<T>() where T : class;
    }
}