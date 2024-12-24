using MVC.Model;
using Services.ServiceResolver;
using UnityEngine;

namespace MVC.Controller
{
    public abstract class BaseController<TModel> : IController where TModel : IModel
    {
        public TModel Model { get; private set; }
        public IServiceLocator ServiceLocator { get; private set; }
        public IAnyTypeResolver AnyTypeResolver { get; private set; }
        public IControllerFactory ControllerFactory { get; private set; }
        
        public ControllerContainer ControllerContainer { get; private set; }

        public BaseController(TModel model)
        {
            Model = model;
            ControllerContainer = new ControllerContainer();
        }

        public void InjectModel(TModel model)
        {
            Model = model;
        }

        public void InjectDependencies(IServiceLocator serviceLocator, IControllerFactory controllerFactory)
        {
            ServiceLocator = serviceLocator;
            ControllerFactory = controllerFactory;
            AnyTypeResolver = ServiceLocator.Resolve<IAnyTypeResolver>();
        }

        public virtual void Dispose()
        {
            ControllerContainer.Dispose();
        }
        
        protected TController Create<TController>(TController controller) where TController : IController
        {
            ControllerFactory.Create(controller);
            ControllerContainer.Add(controller);
            return controller;
        }

        protected TController Create<TController, TModel>(TController controller, TModel model)
            where TController : BaseController<TModel> where TModel : IModel
        {
            controller = ControllerFactory.Create(controller, model);
            ControllerContainer.Add(controller);
            return controller;
        }

        protected TController Create<TController>() where TController : IController, new()
        {
            TController controller = ControllerFactory.Create<TController>();
            ControllerContainer.Add(controller);
            return controller;
        }

        protected TController Create<TController, TModel>(TModel model)
            where TController : BaseController<TModel>, new() where TModel : IModel
        {
            TController controller = ControllerFactory.Create<TController, TModel>(model);
            ControllerContainer.Add(controller);
            return controller;
        }
    }

    public abstract class BaseController : BaseController<EmptyModel>
    {
        public BaseController() : base(new EmptyModel())
        {
        }
    }
}