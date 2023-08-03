namespace Game.SOLevel
{
    using System.Collections;
    using System.Collections.Generic;
    using Edleron.Events;
    using Game.Rhythmic;
    using UnityEngine;
    using System.Linq;
    using Game.PlayerPrefs;
    using Game.Heart;
    using Game.Audio;

    public class LevelManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public static LevelManager Instance { get; private set; }

        public List<LevelDataScriptableObject> levelList;
        private List<int> IndixBlackList = new List<int>();
        private int LevelIndex { get; set; }
        private int RoundIndex { get; set; }
        private int IndisIndex { get; set; }
        private int PullResult { get; set; }
        private bool TutorialLevel = false;

        private void Awake()
        {
            Instance = this;
            LevelIndex = 0;
            PullResult = 0;
            IndisIndex = -1;
            IndixBlackList.Clear();
        }

        private void Start()
        {
            LevelIndex = PlayerPrefsManager.Instance.LoadLevel();
            Debug.Log("Level -> " + LevelIndex);
        }

        #region Level Process
        public void SetTutorialLevel()
        {
            TutorialLevel = false;
        }
        public bool GetTutorialLevel()
        {
            return TutorialLevel;
        }

        public void SetLevelIndex(int value)
        {
            LevelIndex = value;
            PlayerPrefsManager.Instance.SaveLevel(value);
        }
        public int GetLevelIndex()
        {
            return LevelIndex;
        }

        public void SetResult(int value)
        {
            PullResult = value;
        }
        public int GetResult()
        {
            return PullResult;
        }

        public void SetRoundCount(int value)
        {
            RoundIndex = value;
        }
        public int GetRoundCount()
        {
            return RoundIndex;
        }

        public int GetStampCount()
        {
            return levelList[LevelIndex].levelData.VisibleStamp;
        }
        #endregion



        #region Level Prepare
        public bool GetLevelProcess()
        {
            if (LevelIndex < 50)
            {
                return true;
            }
            return false;
        }
        public int GetRotationSpeed()
        {
            return levelList[LevelIndex].levelData.RotatineSpeed;
        }

        public int[] GetPrepareField()
        {
            return levelList[LevelIndex].levelData.PrepareStamp;
        }

        public int[] GetFieldValue()
        {
            int[] values = levelList[LevelIndex].levelData.PrepareValue;
            return ShuffleArray(values);
        }

        public int[] GetPullValue()
        {
            int length = levelList[LevelIndex].levelData.PrepareResult.Length;
            IndisIndex = GenerateRandomIndex(levelList[LevelIndex].levelData.PrepareResult.Length);

            int[] arr = new int[2];
            arr[0] = levelList[LevelIndex].levelData.PrepareResult[IndisIndex].valueOne;
            arr[1] = levelList[LevelIndex].levelData.PrepareResult[IndisIndex].valueTwo;
            SetResult(arr[0] * arr[1]);
            return arr;
        }

        private int GenerateRandomIndex(int length)
        {
            if (IndixBlackList.Count == length)
            {
                return -1;
            }

            int index;
            do
            {
                index = Random.Range(0, length);
            } while (IndixBlackList.Contains(index));

            return index;
        }
        #endregion

        #region BLACK LIST
        public void AddBlackList(int levelIndex, int field)
        {
            int length = levelList[levelIndex].levelData.PrepareResult.Length;
            var data = levelList[levelIndex].levelData.PrepareResult;

            for (int i = 0; i < length; i++)
            {
                if (data[i].result == field)
                {
                    IndixBlackList.Add(i);
                }
            }
        }
        public void ClearBlackList()
        {
            IndisIndex = -1;
            IndixBlackList.Clear();
        }
        #endregion

        #region LevelGenerate
        public void LevelGenerate()
        {
            List<int> data = RhythmicManager.Instance.GetRhytmic();

            if (data.Count > 0)
            {
                Debug.Log("RYTMIC");
                CustomRytmicLevel(data);
            }
            else
            {
                Debug.Log("CUSTOM");
                CustomNewLevel();
            }
        }

        private void CustomRytmicLevel(List<int> data)
        {
            List<int> miniData = TakeFirstEight(data, 4);
            CustomLevel(miniData);
        }

        private void CustomNewLevel()
        {
            List<int> data = GenerateRandomPrepare();
            CustomLevel(data);
        }

        private void CustomLevel(List<int> data)
        {
            if (data.Count > 0)
            {
                // Yeni bir LevelData örneği oluşturun
                LevelData newLevelData = new LevelData();

                // LevelData özelliklerini doldurun
                newLevelData.LevelNumber = GetLevelIndex() + 1;
                newLevelData.LevelName = "Rytmic LEvel";
                newLevelData.LevelDescription = "This is a sample level description.";
                newLevelData.LevelDifficulty = "Difficult";
                newLevelData.TargetScore = 100;
                newLevelData.RotatineSpeed = 70;
                newLevelData.VisibleStamp = data.Count;

                // PrepareStamp dizisini doldurun
                if (data.Count == 8)
                {
                    newLevelData.PrepareStamp = new int[data.Count];
                    for (int j = 0; j < data.Count; j++)
                    {
                        newLevelData.PrepareStamp[j] = j;
                    }
                }
                else
                {
                    newLevelData.PrepareStamp = new int[data.Count];
                    switch (data.Count)
                    {
                        case 1:
                            newLevelData.PrepareStamp[0] = 0;
                            break;
                        case 2:
                            newLevelData.PrepareStamp[0] = 0;
                            newLevelData.PrepareStamp[1] = 4;
                            break;
                        case 3:
                            newLevelData.PrepareStamp[0] = 0;
                            newLevelData.PrepareStamp[1] = 2;
                            newLevelData.PrepareStamp[2] = 5;
                            break;
                        case 4:
                            newLevelData.PrepareStamp[0] = 0;
                            newLevelData.PrepareStamp[1] = 2;
                            newLevelData.PrepareStamp[2] = 4;
                            newLevelData.PrepareStamp[3] = 6;
                            break;
                    }
                }


                // PrepareValue dizisini doldurun
                newLevelData.PrepareValue = new int[data.Count];
                for (int j = 0; j < data.Count; j++)
                {
                    newLevelData.PrepareValue[j] = data[j];
                }

                List<ResultData> resultData = new List<ResultData>();

                for (int j = 0; j < data.Count; j++)
                {
                    var divisors = Enumerable.Range(1, data[j]).Where(x => data[j] % x == 0).ToList();

                    int randomValueOne;
                    int randomValueTwo;

                    RefactorCode(out randomValueOne, out randomValueTwo, divisors);

                    ResultData x = new ResultData();
                    x.result = data[j];
                    x.valueOne = randomValueOne;
                    x.valueTwo = randomValueTwo;

                    resultData.Add(x);
                }

                newLevelData.PrepareResult = resultData.ToArray();

                // Oluşturulan LevelData'yı bir ScriptableObject'e bağlayın
                LevelDataScriptableObject scriptableObject = ScriptableObject.CreateInstance<LevelDataScriptableObject>();
                scriptableObject.levelData = newLevelData;

                levelList.Add(scriptableObject);

#if UNITY_EDITOR
                // ScriptableObject'ı kaydedin (istediğiniz bir dosya yolunu belirleyin)
                string savePath = "Assets/Scripts/Game/SOLevel/Levels/Level-" + (GetLevelIndex()).ToString() + ".asset";
                UnityEditor.AssetDatabase.CreateAsset(scriptableObject, savePath);
                UnityEditor.AssetDatabase.SaveAssets();
                Debug.Log("LevelData asset oluşturuldu ve kaydedildi: " + savePath);
#endif
            }
        }

        private void RefactorCode(out int randomValueOne, out int randomValueTwo, List<int> divisors)
        {
            randomValueOne = -1;
            randomValueTwo = -1;
            if (divisors.Count == 1)
            {
                // 1 -> Bölenleri: 1
                randomValueOne = divisors[0];                       // X
                randomValueTwo = divisors[0];                       // X
            }
            else if (divisors.Count == 2)
            {
                // 3 -> Bölenleri: 1, 3
                randomValueOne = divisors[0];                       // X
                randomValueTwo = divisors[1];                       // X
            }
            else if (divisors.Count == 3)
            {
                // 9 -> Bölenleri: 1, 3, 9
                randomValueOne = divisors[1];                       // 3
                randomValueTwo = divisors[1];                       // 3
            }
            else if (divisors.Count == 4)
            {
                // 10 -> Bölenleri: 1, 2, 5, 10
                randomValueOne = divisors[1];                       // 2
                randomValueTwo = divisors[2];                       // 5
            }
            else if (divisors.Count == 5)
            {
                // 16 -> Bölenleri: 1, 2, 4, 8, 16
                int randomNumberInt = Random.Range(0, 2);           // 0 ile 1 arasında bir tamsayı (100 dahil), 0 ile 1 çıkacaktır.
                switch (randomNumberInt)
                {
                    case 0:
                        randomValueOne = divisors[2];                       // 4
                        randomValueTwo = divisors[2];                       // 4
                        break;
                    case 1:
                        randomValueOne = divisors[1];                       // 2
                        randomValueTwo = divisors[3];                       // 8
                        break;
                }
            }
            else if (divisors.Count == 6)
            {
                // 63 -> Bölenleri: 1, 3, 7, 9, 21, 63
                int randomNumberInt = Random.Range(0, 2);           // 0 ile 1 arasında bir tamsayı (100 dahil), 0 ile 1 çıkacaktır.
                switch (randomNumberInt)
                {
                    case 0:
                        randomValueOne = divisors[1];                       // 3
                        randomValueTwo = divisors[4];                       // 21
                        break;
                    case 1:
                        randomValueOne = divisors[2];                       // 7
                        randomValueTwo = divisors[3];                       // 9
                        break;
                }
            }
            else if (divisors.Count == 7)
            {
                // 64 -> Bölenleri: 1, 2, 4, 8, 16, 32 64 
                int randomNumberInt = Random.Range(0, 2);           // 0 ile 1 arasında bir tamsayı (100 dahil), 0 ile 1 çıkacaktır.
                switch (randomNumberInt)
                {
                    case 0:
                        randomValueOne = divisors[3];                       // 8
                        randomValueTwo = divisors[3];                       // 8
                        break;
                    case 1:
                        randomValueOne = divisors[2];                       // 4
                        randomValueTwo = divisors[4];                       // 16
                        break;
                    case 2:
                        randomValueOne = divisors[1];                       // 2
                        randomValueTwo = divisors[5];                       // 32
                        break;
                }
            }
            else if (divisors.Count == 8)
            {
                // 24 -> Bölenleri: 1, 2, 3, 4, 6, 8, 12, 24
                int randomNumberInt = Random.Range(0, 2);           // 0 ile 1 arasında bir tamsayı (100 dahil), 0 ile 1 çıkacaktır.
                switch (randomNumberInt)
                {
                    case 0:
                        randomValueOne = divisors[3];                       // 4
                        randomValueTwo = divisors[4];                       // 6
                        break;
                    case 1:
                        randomValueOne = divisors[2];                       // 3
                        randomValueTwo = divisors[5];                       // 8
                        break;
                    case 2:
                        randomValueOne = divisors[1];                       // 2
                        randomValueTwo = divisors[6];                       // 12
                        break;
                }
            }
            else if (divisors.Count == 9)
            {
                // 100 -> Bölenleri: 1, 2, 4, 5, 10, 20, 25, 50, 100
                int randomNumberInt = Random.Range(0, 3);           // 0 ile 1 arasında bir tamsayı (100 dahil), 0 ile 1 çıkacaktır.

                switch (randomNumberInt)
                {
                    case 0:
                        randomValueOne = divisors[2];                       // 4
                        randomValueTwo = divisors[6];                       // 25
                        break;
                    case 1:
                        randomValueOne = divisors[3];                       // 5
                        randomValueTwo = divisors[5];                       // 20
                        break;
                    case 2:
                        randomValueOne = divisors[4];                       // 10
                        randomValueTwo = divisors[4];                       // 10
                        break;
                }
            }
            else if (divisors.Count == 10)
            {
                // 48 (Bölenleri: 1, 2, 3, 4, 6, 8, 12, 16, 24, 48)
                int randomNumberInt = Random.Range(0, 3);           // 0 ile 1 arasında bir tamsayı (100 dahil), 0 ile 1 çıkacaktır.

                switch (randomNumberInt)
                {
                    case 0:
                        randomValueOne = divisors[4];                       // 6
                        randomValueTwo = divisors[5];                       // 8
                        break;
                    case 1:
                        randomValueOne = divisors[2];                       // 3
                        randomValueTwo = divisors[7];                       // 16
                        break;
                    case 2:
                        randomValueOne = divisors[3];                       // 4
                        randomValueTwo = divisors[6];                       // 16
                        break;
                    case 3:
                        randomValueOne = divisors[1];                       // 2
                        randomValueTwo = divisors[8];                       // 24
                        break;
                }
            }
            else if (divisors.Count == 11)
            {
                // YOK !
                randomValueOne = divisors[0];                       // !
                randomValueTwo = divisors[divisors.Count - 1];      // !
            }
            else if (divisors.Count == 12)
            {
                // 60 -> Bölenleri: 1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30, 60
                int randomNumberInt = Random.Range(0, 4);           // 0 ile 1 arasında bir tamsayı (100 dahil), 0 ile 1 çıkacaktır.
                switch (randomNumberInt)
                {
                    case 0:
                        randomValueOne = divisors[5];                       // 6
                        randomValueTwo = divisors[6];                       // 10
                        break;
                    case 1:
                        randomValueOne = divisors[2];                       // 3
                        randomValueTwo = divisors[9];                       // 20
                        break;
                    case 2:
                        randomValueOne = divisors[3];                       // 4
                        randomValueTwo = divisors[8];                       // 15
                        break;
                    case 3:
                        randomValueOne = divisors[4];                       // 5
                        randomValueTwo = divisors[7];                       // 12
                        break;
                    case 4:
                        randomValueOne = divisors[1];                       // 2
                        randomValueTwo = divisors[10];                      // 30
                        break;
                }
            }
            else
            {
                Debug.LogError("HATA");
            }
        }
        private List<int> GenerateRandomPrepare()
        {
            List<int> numbersList = new List<int>();
            System.Random random = new System.Random();

            while (numbersList.Count < 8)
            {
                int randomNumber = random.Next(1, 100 + 1);

                if (!numbersList.Contains(randomNumber) && !CheckPrime(randomNumber))
                {
                    numbersList.Add(randomNumber);
                }
            }
            return numbersList;
        }
        private List<List<int>> CheckDivide(List<int> list, int size)
        {
            List<List<int>> dividedLists = new List<List<int>>();
            for (int i = 0; i < list.Count; i += size)
            {
                dividedLists.Add(list.GetRange(i, System.Math.Min(size, list.Count - i)));
            }
            return dividedLists;
        }
        private List<int> TakeFirstEight(List<int> list, int size)
        {
            int countToTake = System.Math.Min(size, list.Count);
            List<int> firstEight = list.GetRange(0, countToTake);
            Debug.Log("TTT ->" + firstEight.Count);
            return firstEight;
        }
        private bool CheckPrime(int num)
        {
            bool isPrime = Enumerable.Range(1, num).Count(x => num % x == 0) == 2;
            return isPrime;
        }
        #endregion


        #region HELPERS
        private int[] ShuffleArray(int[] array)
        {
            System.Random random = new System.Random();

            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }

            return array;
        }
        #endregion
    }
}