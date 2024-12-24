using System;
using MVC.Model;
using UnityEngine;

namespace Game.UserStateManager
{
    [Serializable]
    public class UserStateData : IModel
    {
        [SerializeField] private int _level;
        [SerializeField] private int _timePowerup;
        [SerializeField] private int _autoCompletePowerup;
        
        public int Level => _level;
        public int TimePowerup => _timePowerup;
        public int AutoCompletePowerup => _autoCompletePowerup;

        public UserStateData(int level, int timePowerup, int autoCompletePowerup)
        {
            _level = level;
            _timePowerup = timePowerup;
            _autoCompletePowerup = autoCompletePowerup;
        }

        public void NextLevel()
        {
            _level += 1;
        }

        public void WithdrawTimePowerup()
        {
            _timePowerup -= 1;
        }

        public void WithdrawAutoCompletePowerup()
        {
            _autoCompletePowerup -= 1;
        }
    }
}