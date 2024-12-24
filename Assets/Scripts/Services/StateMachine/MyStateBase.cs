using System.Threading;
using Cysharp.Threading.Tasks;
using Services.ServiceResolver;
using UniState;

namespace Services.StateMachine
{
    public abstract class MyStateBase : StateBase
    {
        protected IServiceLocator ServiceLocator { get; private set; }

        public MyStateBase(IServiceLocator serviceLocator)
        {
            ServiceLocator = serviceLocator;
        }
    }
}