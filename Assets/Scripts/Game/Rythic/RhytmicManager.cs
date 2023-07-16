namespace Game.Rhythmic
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    public class RhythmicManager : MonoBehaviour
    {
        public static RhythmicManager Instance { get; private set; }
        public List<List<GameObject>> dummyList = new List<List<GameObject>>();
        public List<List<string>> checkList = new List<List<string>>();
        public List<GameObject> rhythmicResult_0 = new List<GameObject>();
        public List<GameObject> rhythmicResult_1 = new List<GameObject>();
        public List<GameObject> rhythmicResult_2 = new List<GameObject>();
        public List<GameObject> rhythmicResult_3 = new List<GameObject>();
        public List<GameObject> rhythmicResult_4 = new List<GameObject>();
        public List<GameObject> rhythmicResult_5 = new List<GameObject>();
        public List<GameObject> rhythmicResult_6 = new List<GameObject>();
        public List<GameObject> rhythmicResult_7 = new List<GameObject>();
        public List<GameObject> rhythmicResult_8 = new List<GameObject>();

        private void Awake()
        {
            Instance = this;
            SetDict();
        }

        public void SetDict()
        {
            dummyList.Add(rhythmicResult_0);
            dummyList.Add(rhythmicResult_1);
            dummyList.Add(rhythmicResult_2);
            dummyList.Add(rhythmicResult_3);
            dummyList.Add(rhythmicResult_4);
            dummyList.Add(rhythmicResult_5);
            dummyList.Add(rhythmicResult_6);
            dummyList.Add(rhythmicResult_7);
            dummyList.Add(rhythmicResult_8);

            for (int i = 0; i < dummyList.Count; i++)
            {
                checkList.Add(new List<string>()); // Boş bir liste ekleyin
            }

            foreach (List<GameObject> innerList in dummyList)
            {
                foreach (GameObject gameObject in innerList)
                {
                    Debug.Log(gameObject.name);
                }
            }
        }

        public void SetRhytmic(int level, string result)
        {
            List<GameObject> innerList = dummyList[level];

            string fileName = "Detect - " + result;

            foreach (GameObject obj in innerList)
            {
                if (obj.name == fileName)
                {
                    obj.SetActive(false);
                    checkList[level].Add(obj.name);
                }
            }
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
                        GameObject obj = GameObject.Find(nameList[i][j]);
                        if (obj != null)
                        {
                            dummyList[i].Add(obj);
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
