namespace Game.Level
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.IO;
    using System;

    public class LevelTransmiter
    {
        private int currentLevel;
        private List<LevelData> levelDataList;

        public LevelTransmiter()
        {
            // Seviye verilerini yükle
            levelDataList = LoadLevelDataFromSource();
            currentLevel = 1; // Varsayılan olarak ilk seviye
        }

        public void StartCurrentLevel()
        {
            LevelData currentLevelData = GetLevelData(currentLevel);
            // Seviye başlatma işlemleri
        }

        public void CompleteCurrentLevel()
        {
            // Seviye tamamlama işlemleri

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

        private LevelData GetLevelData(int levelNumber)
        {
            // levelNumber'a göre seviye verilerini döndür
            return levelDataList[currentLevel]; // Todo
        }

        private List<LevelData> LoadLevelDataFromSource()
        {
            string filePath = Application.dataPath + "/LevelData.json";
            Debug.Log("File path: " + filePath);

            if (File.Exists(filePath))
            {
                Debug.Log("File exists");
                try
                {
                    string jsonData = File.ReadAllText(filePath);
                    Debug.Log("JSON data: " + jsonData);

                    LevelDataListWrapper wrapper = JsonUtility.FromJson<LevelDataListWrapper>(jsonData);
                    if (wrapper != null && wrapper.levelDataList != null)
                    {
                        Debug.Log("Data loaded successfully");
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
