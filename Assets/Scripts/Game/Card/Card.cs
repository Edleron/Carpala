namespace Game.Card
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Game.Level;

    public class Card : MonoBehaviour
    {
        // Logic Burası Card Manager Olucak -> // Todo
        // Visual Kısmında -> // Todo {Animasyon, görselleştirme, Açma kapama} -> Event Delegate
        public List<GameObject> stamp = new List<GameObject>();

        private float rotationSpeed = 5.0f;

        private void Awake()
        {
            // levelManager = FindObjectOfType<LevelManager>();
        }


        private void Start()
        {
            rotationSpeed = rotationSpeed * LevelManager.Instance.GetSectionValue();
            foreach (var item in stamp)
            {
                item.SetActive(false);
            }
            SetLevelStamp();
        }

        private void Update()
        {
            CardRotate();
        }

        private void SetLevelStamp()
        {
            var arr = LevelManager.Instance.GetStampValue();
            for (int i = 0; i < arr.Length; i++)
            {
                stamp[arr[i]].SetActive(true);
            }
        }

        private void CardRotate()
        {
            transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        }
    }
}