using System.Collections.Generic;
using MVC.Model;
using UnityEngine;

namespace Game.Gameplay.Data
{
    [CreateAssetMenu(fileName = "LevelsContainerConfig", menuName = "Game/Config/LevelsContainerConfig")]
    public class LevelsContainerConfig : ScriptableObject, IModel
    {
        [SerializeField] private List<LevelConfig> _levels;
        
        public LevelConfig GetLevelConfig(int level)
        {
            int levelsCount = _levels.Count;
            level = (level - 1) % levelsCount;
            return _levels[level];
        }
    }
}