namespace Game.Rhythmic
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    public class RhythmicManager : MonoBehaviour
    {
        public static RhythmicManager Instance { get; private set; }
        public List<List<string>> checkList = new List<List<string>>();
        public List<RhtyhmicDataAO> rhytmicData = new List<RhtyhmicDataAO>();

        private void Awake()
        {
            Instance = this;
            SetDict();
        }

        public void SetDict()
        {
            // Todo
            for (int i = 0; i < rhytmicData.Count; i++)
            {
                checkList.Add(new List<string>()); // Todo
            }
        }

        public void SetRhytmic(int level, string result)
        {
            /*
            List<GameObject> innerList = rhytmicData[level].list;

            string fileName = "Detect - " + result;

            foreach (GameObject obj in innerList)
            {
                if (obj.name == fileName)
                {
                    obj.SetActive(false);
                    checkList[level].Add(obj.name);
                }
            }
            */
        }

        public void SaveResultListToJson(string filePath)
        {
            List<List<string>> nameList = new List<List<string>>();
            foreach (List<string> innerList in checkList)
            {
                List<string> names = new List<string>();
                foreach (string fileName in innerList)
                {
                    names.Add(fileName);
                }
                nameList.Add(names);
            }

            string jsonData = JsonConvert.SerializeObject(nameList, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }

        public void LoadResultListFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                List<List<string>> nameList = JsonConvert.DeserializeObject<List<List<string>>>(jsonData);

                for (int i = 0; i < nameList.Count; i++)
                {
                    for (int j = 0; j < nameList[i].Count; j++)
                    {
                        string objectName = nameList[i][j];
                        GameObject obj = dummyList[i].Find(go => go.name == objectName);
                        if (obj != null)
                        {
                            checkList[i].Add(obj.name);
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("JSON file does not exist: " + filePath);
            }
        }
    }
}
