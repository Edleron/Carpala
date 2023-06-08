namespace Game.JsonLevel
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.IO;
    using System;

    public class LevelTransmiter
    {
        private int currentLevel;
        private int currentSection;
        private List<LevelData> levelDataList;

        public LevelTransmiter()
        {
            currentLevel = 0; // Todo
            currentSection = 5; // Todo
            levelDataList = LoadLevelDataFromSource();
        }

        public void StartCurrentLevel()
        {
            LevelData currentLevelData = GetLevelData();
        }

        public void CompleteCurrentLevel()
        {
            if (currentLevel < levelDataList.Count)
            {
                currentLevel++;
                StartCurrentLevel();
            }
            else
            {
                // Tüm seviyeler tamamlandı, oyunu bitir
            }
        }

        public void CompleteCurrentSection()
        {
            if (currentSection < levelDataList[currentLevel].Section)
            {
                currentSection++;
            }
            else
            {
                CompleteCurrentLevel();
            }
        }

        public LevelData GetLevelData()
        {
            return levelDataList[currentLevel]; // Todo
        }

        public int GetSectionData()
        {
            return currentSection;
        }

        private List<LevelData> LoadLevelDataFromSource()
        {
            string filePath = Application.dataPath + "/LevelData.json"; // Todo
            if (File.Exists(filePath))
            {
                try
                {
                    string jsonData = File.ReadAllText(filePath);

                    // Debug.Log("JSON data: " + jsonData);

                    LevelDataListWrapper wrapper = JsonUtility.FromJson<LevelDataListWrapper>(jsonData);
                    if (wrapper != null && wrapper.levelDataList != null)
                    {
                        return wrapper.levelDataList;
                    }
                    else
                    {
                        Debug.LogError("Invalid JSON data or missing level data in the JSON file");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Error reading JSON file: " + e.Message);
                }
            }
            else
            {
                Debug.LogError("JSON file does not exist at the specified path: " + filePath);
            }

            return new List<LevelData>();
        }
    }
}
