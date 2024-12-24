using Cysharp.Threading.Tasks;
using Services.ServiceResolver;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Services.ResourceProvider
{
    public class AddressablesResourceProvider : BaseService, IResourceProvider
    {
        public void Initialize(ServiceLocator serviceLocator)
        {
            base.Initialize(serviceLocator);
            var handle = Addressables.InitializeAsync();
            handle.WaitForCompletion();
        }
        
        public async UniTask<T> LoadAssetAsync<T>(string name) where T : Object
        {
            var handle = Addressables.LoadAssetAsync<Object>(name);
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null)
            {
                if (typeof(T).IsSubclassOf(typeof(ScriptableObject)))
                {
                    return handle.Result as T;
                }

                return (handle.Result as GameObject)?.GetComponent<T>();
            }
            return null;
        }

        public T LoadAsset<T>(string name) where T : Object
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(name);
            handle.WaitForCompletion();
            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null)
            {
                return handle.Result.GetComponent<T>();
            }
            return null;
        }

        public void Release<T>(T asset) where T : Object
        {
            Addressables.Release(asset);
        }
    }
}