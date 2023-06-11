namespace Game.SOLevel
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LevelManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public static LevelManager Instance { get; private set; }

        public List<LevelDataScriptableObject> levelList;

        private int LevelIndex { get; set; }

        private void Awake()
        {
            Instance = this;
            LevelIndex = 0; // Todo PlayerPrefs
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

        public int[] GetPrepareStamp()
        {
            return levelList[LevelIndex].levelData.PrepareStamp;
        }

        public int[] GetPrepareValue()
        {
            int[] values = levelList[LevelIndex].levelData.PrepareValue;
            return ShuffleArray(values);
        }

        public int[] GetPrepareResult()
        {
            int length = levelList[LevelIndex].levelData.PrepareResult.Length;
            int indis = Random.Range(1, length);

            int[] arr = new int[2];
            arr[0] = levelList[LevelIndex].levelData.PrepareResult[indis].valueOne;
            arr[1] = levelList[LevelIndex].levelData.PrepareResult[indis].valueTwo;
            return arr;
        }
        #endregion

        #region HELPERS
        public int[] ShuffleArray(int[] array)
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