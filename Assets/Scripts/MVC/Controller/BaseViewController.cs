using Cysharp.Threading.Tasks;
using MVC.Model;
using MVC.View;
using Services.ResourceProvider;
using UnityEngine;

namespace MVC.Controller
{
    public abstract class BaseViewController<TView, TModel> : BaseController<TModel> where TView : BaseView where TModel : IModel
    {
        public abstract string AssetName { get; }
        public TView View { get; private set; }
        
        protected BaseViewController(TModel model) : base(model)
        {
        }
        
        public override void Dispose()
        {
            if (!View.IsDisposed)
            {
                View.Dispose();
                Object.Destroy(View?.gameObject);
            }
            base.Dispose();
        }

        public virtual async UniTask InitializeAsync()
        {
            await LoadView();
        }
        
        public virtual void Initialize()
        {
            LoadView().Forget();
        }

        protected abstract void OnViewLoaded();

        private async UniTask LoadView()
        {
            var resourceProvider = ServiceLocator.Resolve<IResourceProvider>();
            var result = await resourceProvider.LoadAssetAsync<TView>(AssetName);
            if (result != null)
            {
                View = Object.Instantiate(result);
                OnViewLoaded();
            }
            else
            {
                Debug.LogError($"[BaseViewController] Failed to load view: {AssetName}  Type: {typeof(TView).Name}");
            }
        }
    }

    public abstract class BaseViewController<TView> : BaseViewController<TView, EmptyModel> where TView : BaseView
    {
        public BaseViewController() : base(new EmptyModel())
        {
        }
    }
}