namespace Game.SOLevel
{
    using System.Collections;
    using System.Collections.Generic;
    using Edleron.Events;
    using UnityEngine;

    public class LevelManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public static LevelManager Instance { get; private set; }

        public List<LevelDataScriptableObject> levelList;

        private int LevelIndex { get; set; }
        private int RoundIndex { get; set; }
        private int PullResult { get; set; }

        private void Awake()
        {
            Instance = this;
            LevelIndex = 0; // Todo PlayerPrefs
            PullResult = 0; // Todo PlayerPrefs
        }

        #region Level Process
        public void SetLevel()
        {
            LevelIndex++;
        }

        public LevelData GetLevel()
        {
            return levelList[LevelIndex].levelData;
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
            int indis = Random.Range(1, length);

            int[] arr = new int[2];
            arr[0] = levelList[LevelIndex].levelData.PrepareResult[indis].valueOne;
            arr[1] = levelList[LevelIndex].levelData.PrepareResult[indis].valueTwo;
            PullResult = arr[0] * arr[1];
            return arr;
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
            Debug.Log(PullResult.ToString() + " " + fieldResult.ToString());

            EventManager.Fire_onExplosion();

            if (PullResult == fieldResult)
            {
                EventManager.Fire_onCorrect();
            }
            else
            {
                EventManager.Fire_onWrong();
            }

            Invoke("NewRound", 1.0f);
        }

        private void NewRound()
        {
            RoundIndex++;
            EventManager.Fire_onSwipeDown();
            EventManager.Fire_onCheckLevel();
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