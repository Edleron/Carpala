namespace Game.SOLevel
{
    using System.Collections;
    using System.Collections.Generic;
    using Edleron.Events;
    using Game.Rhythmic;
    using UnityEngine;

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
        private bool TutorialLevel = true;

        private void Awake()
        {
            Instance = this;
            LevelIndex = 0; // Todo PlayerPrefs
            PullResult = 0; // Todo PlayerPrefs
            IndisIndex = -1;
            IndixBlackList.Clear();
        }

        #region Level Process
        public void SetLevelIndex()
        {
            LevelIndex++;
        }
        public int GetLevelIndex()
        {
            return LevelIndex;
        }

        public LevelData GetLevel()
        {
            return levelList[LevelIndex].levelData;
        }
        public bool GetTutorialLevel()
        {
            return TutorialLevel;
        }
        public void SetTutorialLevel()
        {
            TutorialLevel = false;
        }
        #endregion


        #region Level Prepare
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
            // Todo
            int[] values = levelList[LevelIndex].levelData.PrepareValue;
            return ShuffleArray(values);
        }

        public int[] GetPullValue()
        {
            // Todo
            int length = levelList[LevelIndex].levelData.PrepareResult.Length;
            IndisIndex = GenerateRandomIndex(levelList[LevelIndex].levelData.PrepareResult.Length);

            int[] arr = new int[2];
            arr[0] = levelList[LevelIndex].levelData.PrepareResult[IndisIndex].valueOne;
            arr[1] = levelList[LevelIndex].levelData.PrepareResult[IndisIndex].valueTwo;
            PullResult = arr[0] * arr[1];
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

        public int GetStampCount()
        {
            return levelList[LevelIndex].levelData.VisibleStamp;
        }
        public int GetRoundCount()
        {
            return RoundIndex;
        }
        public void SetRoundCount()
        {
            RoundIndex = 0;
        }
        #endregion

        #region Level Round
        public void CheckResult(int fieldResult)
        {
            // Debug.Log(PullResult.ToString() + " " + fieldResult.ToString()); // TODO

            RoundIndex++;

            EventManager.Fire_onExplosion();

            if (PullResult == fieldResult)
            {
                EventManager.Fire_onCorrect();

                int length = levelList[LevelIndex].levelData.PrepareResult.Length;
                var data = levelList[LevelIndex].levelData.PrepareResult;

                for (int i = 0; i < length; i++)
                {
                    if (data[i].result == PullResult)
                    {
                        IndixBlackList.Add(i);
                    }
                }

                Debug.Log("IndixBlackList Values:");
                foreach (int value in IndixBlackList)
                {
                    Debug.Log(value);
                }

                RhythmicManager.Instance.SetRhytmic(LevelIndex, fieldResult.ToString());
            }
            else
            {
                EventManager.Fire_onWrong();
            }

            Invoke("NewRound", 0.25f);
        }

        private void NewRound()
        {


            int stamp = GetStampCount();
            int round = GetRoundCount();

            if (stamp == round)
            {
                IndisIndex = -1;
                SetRoundCount();
                SetLevelIndex();
                IndixBlackList.Clear();
                EventManager.Fire_onEndLevel();
            }
            // TODO
            /*
            else
            {
                // IndisIndex = -1;
                // EventManager.Fire_onRepeatLevel();
                // SetRoundCount();
                // IndixBlackList.Clear();
            }
            */

            EventManager.Fire_onSwipeDown();
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