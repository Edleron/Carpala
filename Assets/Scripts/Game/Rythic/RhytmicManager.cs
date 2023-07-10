namespace Game.Rhythmic
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RhytmicManager : MonoBehaviour
    {
        public static RhytmicManager Instance { get; private set; }
        public List<GameObject> rhythmicResult_0 = new List<GameObject>();
        public List<GameObject> rhythmicResult_1 = new List<GameObject>();
        public List<GameObject> rhythmicResult_2 = new List<GameObject>();
        public List<GameObject> rhythmicResult_3 = new List<GameObject>();
        public List<GameObject> rhythmicResult_4 = new List<GameObject>();
        public List<GameObject> rhythmicResult_5 = new List<GameObject>();
        public List<GameObject> rhythmicResult_6 = new List<GameObject>();
        public List<GameObject> rhythmicResult_7 = new List<GameObject>();
        public List<GameObject> rhythmicResult_8 = new List<GameObject>();
        private Dictionary<string, bool> rhythmicDict = new Dictionary<string, bool>();

        private void Awake()
        {
            Instance = this;
            SetDict(0);
        }

        public void SetDict(int level)
        {
            switch (level)
            {
                case 0:
                    foreach (GameObject obj in rhythmicResult_0)
                    {
                        string fileName = obj.name;
                        if (!rhythmicDict.ContainsKey(fileName))
                        {
                            rhythmicDict.Add(fileName, false);
                        }
                    }
                    break;
                case 1:
                    foreach (GameObject obj in rhythmicResult_1)
                    {
                        string fileName = obj.name;
                        if (!rhythmicDict.ContainsKey(fileName))
                        {
                            rhythmicDict.Add(fileName, false);
                        }
                    }
                    break;
                case 2:
                    foreach (GameObject obj in rhythmicResult_2)
                    {
                        string fileName = obj.name;
                        if (!rhythmicDict.ContainsKey(fileName))
                        {
                            rhythmicDict.Add(fileName, false);
                        }
                    }
                    break;
                case 3:
                    foreach (GameObject obj in rhythmicResult_3)
                    {
                        string fileName = obj.name;
                        if (!rhythmicDict.ContainsKey(fileName))
                        {
                            rhythmicDict.Add(fileName, false);
                        }
                    }
                    break;
                case 4:
                    foreach (GameObject obj in rhythmicResult_4)
                    {
                        string fileName = obj.name;
                        if (!rhythmicDict.ContainsKey(fileName))
                        {
                            rhythmicDict.Add(fileName, false);
                        }
                    }
                    break;
                case 5:
                    foreach (GameObject obj in rhythmicResult_5)
                    {
                        string fileName = obj.name;
                        if (!rhythmicDict.ContainsKey(fileName))
                        {
                            rhythmicDict.Add(fileName, false);
                        }
                    }
                    break;
                case 6:
                    foreach (GameObject obj in rhythmicResult_6)
                    {
                        string fileName = obj.name;
                        if (!rhythmicDict.ContainsKey(fileName))
                        {
                            rhythmicDict.Add(fileName, false);
                        }
                    }
                    break;
                case 7:
                case 8:
                    foreach (GameObject obj in rhythmicResult_7)
                    {
                        string fileName = obj.name;
                        if (!rhythmicDict.ContainsKey(fileName))
                        {
                            rhythmicDict.Add(fileName, false);
                        }
                    }
                    break;
                case 9:
                case 10:
                    foreach (GameObject obj in rhythmicResult_8)
                    {
                        string fileName = obj.name;
                        if (!rhythmicDict.ContainsKey(fileName))
                        {
                            rhythmicDict.Add(fileName, false);
                        }
                    }
                    break;
            }           
        }        

        public void SetRhytmic(int level, string result)
        {
            string fileName = "Detect - " + result;
            if (rhythmicDict.ContainsKey(fileName))
            {
                rhythmicDict[fileName] = true;
            }

            foreach (GameObject obj in rhythmicResult_1)
            {
                string objName = obj.name;
                if (objName == fileName)
                {
                    obj.SetActive(false);
                }
            }
        }

        public bool GetRhytmic()
        {
            foreach (bool isActive in rhythmicDict.Values)
            {
                if (!isActive)
                {
                    return false;
                }
            }

            rhythmicDict.Clear();

            return true;
        }
    }
}

// NieR:Automata™
// Immortals Fenyx Rising
// Dragon Age™ Inquisition
// Yaga
// Red Dead Redemption 2
// Assassin's Creed Valhalla
// FOR HONOR™
// Tails of Iron
// Hellblade: Senua's Sacrifice
// Bloodstained: Ritual of the Night
// Don't Starve
// Oxygen Not Included
// Roadwarden
// Celeste
// Disco Elysium - The Final Cut