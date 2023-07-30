namespace Game.Rhythmic
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    public class RhythmicManager : MonoBehaviour
    {
        public static RhythmicManager Instance { get; private set; }
        public List<RhtyhmicDataGM> rhytmicObje = new List<RhtyhmicDataGM>();

        public RhythmicDataScriptableObject rhytmicSo;

        private void Awake()
        {
            // rhytmicObje[0].list[0].Status = false;
            // rhytmicSo.rhytmicData[0].list[0].Status = false;
            Instance = this;
        }

        public void ActiveRhytmic(int level, string result)
        {
            string fileName = "Detect - " + result;

            if (level < 0 || level >= rhytmicObje.Count)
            {
                foreach (var item in rhytmicObje)
                {
                    foreach (var x in item.list)
                    {
                        if (x.RhythmicName == fileName)
                        {
                            x.Status = true;
                            x.Kratus.SetActive(false);
                        }
                    }
                }

                foreach (var item in rhytmicSo.rhytmicData)
                {
                    foreach (var x in item.list)
                    {
                        if (x.RhythmicName == fileName)
                        {
                            x.Status = true;
                        }
                    }
                }
                return;
            }

            // LEVEL 10' Sonrası
            RhtyhmicDataGM levelData = rhytmicObje[level];
            List<RhtyhmicDataGameMode> innerList = levelData.list;

            foreach (RhtyhmicDataGameMode data in innerList)
            {
                if (data.RhythmicName == fileName)
                {
                    data.Status = true;
                    data.Kratus.SetActive(false);
                }
            }

            RhtyhmicDataScriptableMode t = rhytmicSo.rhytmicData[level].list.Find(item => item.RhythmicName == fileName);
            t.Status = true;
        }

        public List<int> GetRhytmic()
        {
            List<int> newData = new List<int>();

            foreach (RhtyhmicDataSO dataSO in rhytmicSo.rhytmicData)
            {
                foreach (RhtyhmicDataScriptableMode data in dataSO.list)
                {
                    if (!data.Status)
                    {
                        // Debug.Log("RhythmicName: " + data.RhythmicName + ", Status: " + data.Status);

                        string[] parts = data.RhythmicName.Split(' ');
                        if (parts.Length >= 3 && int.TryParse(parts[2].Trim(), out int numericValue))
                        {
                            newData.Add(numericValue);
                        }
                    }
                }
            }

            return newData;
        }

        public int CorrectRhytmic(int level)
        {
            if (level > 10)
            {
                return -1;
            }

            RhtyhmicDataSO levelData = rhytmicSo.rhytmicData[level];
            List<RhtyhmicDataScriptableMode> innerList = levelData.list;

            foreach (RhtyhmicDataScriptableMode data in innerList)
            {
                if (data.Status == false)
                {
                    return 0;
                }
            }
            return 1;
        }
    }
}
