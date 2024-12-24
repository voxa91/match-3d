using Cysharp.Threading.Tasks;
using Services.ServiceResolver;
using UnityEngine;

namespace Services.ResourceProvider
{
    public interface IResourceProvider : IService
    {
        public UniTask<T> LoadAssetAsync<T>(string name) where T : Object;
        public T LoadAsset<T>(string name) where T : Object;
        public void Release<T>(T asset) where T : Object;
    }
}