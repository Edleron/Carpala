namespace Game.Stars
{
    using System.Collections;
    using UnityEngine;
    using Game.Speed;
    using Edleron.Events;
    using TMPro;

    public class StarVisualizer : MonoBehaviour
    {
        public static StarVisualizer Instance { get; private set; }

        private Animator anim;
        private TextMeshPro textObje;
        private int Counter = 60;
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
            return Counter;
        }
        public void SetCount(int value)
        {
            Counter = value;
        }

        public void CounterLauncher()
        {
            anim.SetBool("Active", true);
        }

        public void CounterCover()
        {
            anim.SetBool("Active", false);
        }

        private void SetText()
        {
            Counter--;
            textObje.text = Counter.ToString();
            SpeedVisualizer.Instance.CountSliderValue();

            if (Counter <= 0)
            {
                CounterCover();
                EventManager.Fire_onFinishedTime();
            }
        }

        public void ScoreTimerUpdated()
        {
            CounterCover();
            elapsedTime = 0f;
            targetScore = Counter != 60 ? (Counter - 10) : Counter;
            StartCoroutine(AnimatedScore());
        }

        private IEnumerator AnimatedScore()
        {
            while (elapsedTime < during)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / during);
                int newScore = Mathf.RoundToInt(Mathf.Lerp(currentScore, targetScore, t));
                textObje.text = newScore.ToString();
                yield return null;
            }
            textObje.text = targetScore.ToString();
            currentScore = targetScore;
            Counter = currentScore;
            CounterLauncher();
        }
    }
}