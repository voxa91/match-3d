using Cysharp.Threading.Tasks;
using Game.Gameplay.Data;
using MVC.Controller;
using Services.ResourceProvider;

namespace Game.Gameplay.Controller
{
    public class ConfigsProvider : BaseController
    {
        public LevelsContainerConfig LevelsContainerConfig { get; private set; }
        public GameplayItemsConfig GameplayItemsConfig { get; private set; }

        public void Initialize()
        {
            LoadConfigs().Forget();
        }

        private async UniTaskVoid LoadConfigs()
        {
            var resourceProvider = ServiceLocator.Resolve<IResourceProvider>();
            LevelsContainerConfig =
                await resourceProvider.LoadAssetAsync<LevelsContainerConfig>(nameof(LevelsContainerConfig));
            GameplayItemsConfig =
                await resourceProvider.LoadAssetAsync<GameplayItemsConfig>(nameof(GameplayItemsConfig));
        }

        public LevelConfig GetCurrentLevelConfig()
        {
            var userStateManager = AnyTypeResolver.Resolve<UserStateManager.UserStateManager>();
            int level = userStateManager.UserStateData.Level;
            return LevelsContainerConfig.GetLevelConfig(level);
        }
    }
}