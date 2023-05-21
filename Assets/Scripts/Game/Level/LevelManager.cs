namespace Game.Level
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LevelManager : MonoBehaviour
    {
        private LevelTransmiter levelTransmiter;

        private void Start()
        {
            levelTransmiter = new LevelTransmiter();
            levelTransmiter.StartCurrentLevel();
            Invoke("levelTransmiter.CompleteCurrentLevel", 2);
        }

        private void Update()
        {
            // Oyun içerisinde gerekli koşullara göre seviye durumları değiştirilir

        }
    }
}