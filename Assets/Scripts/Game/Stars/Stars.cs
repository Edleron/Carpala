

namespace Game.Skore
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;
    using TMPro;

    public class Stars : MonoBehaviour
    {
        private Animator anim;
        private TextMeshPro textObje;
        private int currentScore = 0;
        private int targetScore = 0;
        private float elapsedTime = 0f;
        private float during = 2.0f;
        private void Awake()
        {
            targetScore = 100;
            currentScore = 0;
            anim = GetComponent<Animator>();
            textObje = this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
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

        private void PlayCorrectSkore()
        {
            targetScore += 10;
            anim.SetBool("Active", true);
            StartScoreAnimation();
            Invoke("CloseAnim", during);
        }

        private void PlayWrongSkore()
        {
            targetScore -= 10;
            StartScoreAnimation();
            anim.SetBool("Active", true);
            Invoke("CloseAnim", during);
        }

        private void StartScoreAnimation()
        {
            elapsedTime = 0f;
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
        }

        private void CloseAnim()
        {
            anim.SetBool("Active", false);
        }
    }

}