using MVC.Controller;
using MVC.Model;
using Services.ServiceResolver;

namespace MVC
{
    public interface IControllerFactory : IService
    {
        TController Create<TController, TModel>(TController controller, TModel model) where TController :
            BaseController<TModel> where TModel : IModel;

        TController Create<TController>(TController controller) where TController : IController;

        TController Create<TController, TModel>(TModel model) where TController :
            BaseController<TModel>, new() where TModel : IModel;

        TController Create<TController>() where TController : IController, new();
    }
}