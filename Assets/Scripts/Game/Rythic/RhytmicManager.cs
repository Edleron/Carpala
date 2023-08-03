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
            Instance = this;
        }

        private void Start()
        {
            AwakeRhytmic();
        }

        private void AwakeRhytmic()
        {
            for (var i = 0; i < rhytmicSo.rhytmicData.Count; i++)
            {
                for (var j = 0; j < rhytmicSo.rhytmicData[i].list.Count; j++)
                {
                    if (rhytmicSo.rhytmicData[i].list[j].Status == true)
                    {
                        string resName = rhytmicSo.rhytmicData[i].list[j].RhythmicName;

                        List<RhtyhmicDataGameMode> test = rhytmicObje[i].list.FindAll(x => x.RhythmicName == resName);

                        for (var f = 0; f < test.Count; f++)
                        {
                            test[f].Kratus.SetActive(false);
                            test[f].Status = true;
                        }
                    }
                }
            }
        }

        public void RestartRhytmic()
        {
            for (var i = 0; i < rhytmicSo.rhytmicData.Count; i++)
            {
                for (var j = 0; j < rhytmicSo.rhytmicData[i].list.Count; j++)
                {
                    rhytmicSo.rhytmicData[i].list[j].Status = false;
                }
            }

            for (var i = 0; i < rhytmicObje.Count; i++)
            {
                for (var j = 0; j < rhytmicObje[i].list.Count; j++)
                {
                    rhytmicObje[i].list[j].Kratus.SetActive(true);
                    rhytmicObje[i].list[j].Status = false;
                }
            }
        }

        public void ActiveRhytmic(int level = -1, string result = "None")
        {
            string fileName = "Detect - " + result;
            if (level != -1)
            {
                for (var i = 0; i < rhytmicSo.rhytmicData[level].list.Count; i++)
                {
                    if (rhytmicSo.rhytmicData[level].list[i].RhythmicName == fileName)
                    {
                        rhytmicSo.rhytmicData[level].list[i].Status = true;
                    }
                }

                for (var i = 0; i < rhytmicObje[level].list.Count; i++)
                {
                    if (rhytmicObje[level].list[i].RhythmicName == fileName)
                    {
                        rhytmicObje[level].list[i].Status = true;
                        rhytmicObje[level].list[i].Kratus.SetActive(false);
                    }
                }
            }
            else
            {
                foreach (var item in rhytmicSo.rhytmicData)
                {
                    foreach (var xItem in item.list)
                    {
                        if (xItem.RhythmicName == fileName)
                        {
                            xItem.Status = true;
                        }
                    }
                }

                foreach (var item in rhytmicObje)
                {
                    foreach (var xItem in item.list)
                    {
                        if (xItem.RhythmicName == fileName)
                        {
                            xItem.Kratus.SetActive(false);
                            xItem.Status = true;
                        }
                    }
                }
            }
        }

        public void ActiveHardRhytmic(int level, string result)
        {
            string fileName = "Detect - " + result;


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
