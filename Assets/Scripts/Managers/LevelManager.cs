using UnityEngine;
using System.Collections.Generic;
using HappyGarden.UI;

namespace HappyGarden.Gameplay
{
    internal class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private string pathToLevels = "";

        [SerializeField] private List<GameObject> levels = new List<GameObject>();

        private GameObject currentLevel;

        private void Start()
        {
            SetLevels();

            CreateLevel();
        }

        private void SetLevels() 
        {
            GameObject[] objects = Resources.LoadAll<GameObject>(pathToLevels);

            for (int i = 0; i < objects.Length; i++)
            {
                levels.Add(objects[i]);
            }
        }

        internal void CreateLevel() 
        {
            if (currentLevel != null)
            {
                Destroy(currentLevel);
            }

            int levelNumber = ProfileManager.Instance.GetLastUnlockedLevel();

            UIMain.Instance.SetNewLevelText(levelNumber+1);

            currentLevel = Instantiate(levels[levelNumber]);
        }

        internal void IncreaseLastUnlockedLevel() 
        {
            int levelNumber = ProfileManager.Instance.GetLastUnlockedLevel();

            if (levelNumber < levels.Count-1)
            {
                ProfileManager.Instance.IncreaseLastUnlockLevel();
            }
        }
    }
}
