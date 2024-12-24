using MVC.Controller;
using MVC.Model;
using Services.ServiceResolver;
using UnityEngine;

namespace MVC
{
    public class ControllerFactory : BaseService, IControllerFactory
    {
        public TController Create<TController, TModel>(TController controller, TModel model) where TController : 
            BaseController<TModel> where TModel : IModel
        {
            controller.InjectDependencies(ServiceLocator, this);
            controller.InjectModel(model);
            return controller;
        }

        public TController Create<TController>(TController controller) where TController : IController
        {
            controller.InjectDependencies(ServiceLocator, this);
            return controller;
        }

        public TController Create<TController, TModel>(TModel model) where TController :
            BaseController<TModel>, new() where TModel : IModel
        {
            TController controller = Create<TController>();
            controller.InjectModel(model);
            return controller;
        }

        public TController Create<TController>() where TController : IController, new()
        {
            TController controller = new TController();
            controller.InjectDependencies(ServiceLocator,this);
            return controller;
        }
    }
}