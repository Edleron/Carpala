namespace Game.Stars
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Game.Speed;
    using Edleron.Events;
    using TMPro;
    public class StarVisualizer : MonoBehaviour
    {
        public static StarVisualizer Instance { get; private set; }

        private Animator anim;
        private TextMeshPro textObje;
        private int count = 140;
        private Coroutine counterCoroutine;


        private int currentScore = 0;
        private int targetScore = 0;
        private float elapsedTime = 0f;
        private float during = 0.5f;


        private void Awake()
        {
            Instance = this;
            targetScore = 0;
            currentScore = 0;
            anim = GetComponent<Animator>();
            textObje = this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        }

        public int GetCount()
        {
            return count;
        }
        public void SetCount(int value)
        {
            count = value;
            StartingCounter();
        }

        public void StartingCounter()
        {
            counterCoroutine = StartCoroutine(StartCounter());
        }

        private IEnumerator StartCounter()
        {
            while (true)
            {
                anim.SetTrigger("Active");
                yield return new WaitForSeconds(1f); // 1 saniye bekle
            }
        }

        private void SetText()
        {
            count--;
            textObje.text = count.ToString();
            SpeedVisualizer.Instance.CountSliderValue();

            if (count <= 0)
            {
                count = 140;
                StopCounter();
                EventManager.Fire_onEndLevel();
            }
        }

        public void StopCounter()
        {
            // Coroutine'u durdur
            if (counterCoroutine != null)
            {
                StopCoroutine(counterCoroutine);
                counterCoroutine = null;
            }
        }

        public void StartTimeAnimation()
        {
            StopCounter();
            elapsedTime = 0f;
            targetScore = (count - 20);
            StartCoroutine(AnimateScore());
        }

        private IEnumerator AnimateScore()
        {
            while (elapsedTime < during)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / during);

                // Yeni skoru hesapla ve UI üzerinde güncelle
                int newScore = Mathf.RoundToInt(Mathf.Lerp(currentScore, targetScore, t));
                textObje.text = newScore.ToString();

                yield return null;
            }

            // Son skoru göster
            textObje.text = targetScore.ToString();
            currentScore = targetScore;
            count = currentScore;
            StartingCounter();
        }

        private void OnEnable()
        {
            StartingCounter();
        }

        private void OnDisable()
        {
            StopCounter();
        }
    }
}