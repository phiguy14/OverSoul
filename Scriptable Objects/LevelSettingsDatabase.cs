using System.Collections.Generic;
using UnityEngine;

namespace LevelManager
{
    public class LevelSettingsDatabase
    {
        public static LevelSetting[] levelSettings;
        public static Dictionary<string, LevelSetting> GetLevel = new Dictionary<string, LevelSetting>();

        public LevelSettingsDatabase()
        {
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < levelSettings.Length; i++)
            {
                var level = levelSettings[i].levelName;
                GetLevel.Add(level, levelSettings[i]);
            }
        }

    }
}