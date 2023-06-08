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

        private void Awake()
        {
            Instance = this;
            Debug.Log(GetLevelName());
        }

        public string GetLevelName()
        {
            return levelList[0].levelData.LevelName;
        }

        public int GetRotationSpeed()
        {
            return levelList[0].levelData.RotatineSpeed;
        }

        public int[] GetPrepareStamp()
        {
            return levelList[0].levelData.PrepareStamp;
        }

    }
}