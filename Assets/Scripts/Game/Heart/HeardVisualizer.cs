namespace Game.Heart
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;
    using Game.PlayerPrefs;
    using TMPro;

    public class HeardVisualizer : MonoBehaviour
    {
        public static HeardVisualizer Instance { get; private set; }
        private Animator anim;
        private TextMeshPro textObje;
        private int currentScore = 0;
        private int targetScore = 0;
        private float elapsedTime = 0f;
        private float during = 2.0f;

        private void Awake()
        {
            Instance = this;
            anim = GetComponent<Animator>();
            targetScore = 0;
            currentScore = 0;
            textObje = this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();

        }
        private void Start()
        {
            currentScore = PlayerPrefsManager.Instance.LoadHeart();
            textObje.text = currentScore.ToString();
            targetScore = currentScore;
            Debug.Log("Heart -> " + currentScore);
        }

        private void OnEnable()
        {
            EventManager.onCorrect += PlayCorrectSkore;
            EventManager.onWrong += PlayWrongSkore;
        }

        private void OnDisable()
        {
            EventManager.onCorrect -= PlayCorrectSkore;
            EventManager.onWrong -= PlayWrongSkore;
        }

        public int GetSkore()
        {
            return currentScore;
        }

        public void SetSkore(int value)
        {
            targetScore = value;
            Invoke("CloseAnim", during);
            anim.SetBool("Active", true);

            PlayerPrefsManager.Instance.SaveHeart(targetScore);
            ScoreTimerUpdated();
        }

        private void PlayCorrectSkore()
        {
            // TODO
            // targetScore += 1;
            // Invoke("CloseAnim", during);
            // anim.SetBool("Active", true);

            // PlayerPrefsManager.Instance.SaveHeart(targetScore);
            // ScoreTimerUpdated();
        }

        public void PlayWrongSkore()
        {
            targetScore -= 1;
            Invoke("CloseAnim", during);
            anim.SetBool("Active", true);

            PlayerPrefsManager.Instance.SaveHeart(targetScore);
            ScoreTimerUpdated();
        }

        private void ScoreTimerUpdated()
        {
            elapsedTime = 0f;
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
        }

        private void CloseAnim()
        {
            anim.SetBool("Active", false);
        }
    }
}