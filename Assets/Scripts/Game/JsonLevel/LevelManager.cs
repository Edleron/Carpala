namespace Game.JsonLevel
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        private LevelTransmiter levelTransmiter;

        private void Awake()
        {
            Instance = this;
            levelTransmiter = new LevelTransmiter();
        }

        private void Start()
        {
            levelTransmiter.StartCurrentLevel();
        }

        private void Update()
        {

        }

        public int[] GetStampValue()
        {
            int[] arr = levelTransmiter.GetLevelData().PrepareStamp;
            return arr;
        }

        public int GetSectionValue()
        {
            int value = levelTransmiter.GetSectionData();
            return value;
        }
    }
}