using MVC.Controller;
using UnityEngine;

namespace Game.UserStateManager
{
    public class UserStateManager : BaseController
    {
        private const string UserStateManagerKey = "UserStateManager";
        public UserStateData UserStateData { get; private set; }

        public void Initialize()
        {
            if (!TryLoadUserState())
            {
                CreateDefaultUserState();
            }
        }

        public override void Dispose()
        {
            SaveUserState();
            base.Dispose();
        }

        private bool TryLoadUserState()
        {
            if (PlayerPrefs.HasKey(UserStateManagerKey))
            {
                string jsonData = PlayerPrefs.GetString(UserStateManagerKey);
                UserStateData = JsonUtility.FromJson<UserStateData>(jsonData);
                return true;
            }

            return false;
        }

        private void CreateDefaultUserState()
        {
            UserStateData = new UserStateData(1, 10, 10);
        }

        private void SaveUserState()
        {
            string jsonData = JsonUtility.ToJson(UserStateData);
            PlayerPrefs.SetString(UserStateManagerKey, jsonData);
        }

        public void IncreaseLevel()
        {
            UserStateData.NextLevel();
            SaveUserState();
        }
    }
}