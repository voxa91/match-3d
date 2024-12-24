using System;
using Services.ServiceResolver;

namespace MVC.Controller
{
    public interface IController : IDisposable
    {
        IServiceLocator ServiceLocator { get; }
        IControllerFactory ControllerFactory { get; }

        void InjectDependencies(IServiceLocator serviceLocator, IControllerFactory controllerFactory);
    }
}