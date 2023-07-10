namespace Game.Stars
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    public class StarVisualizer : MonoBehaviour
    {
        public static StarVisualizer Instance { get; private set; }

        private Animator anim;
        private TextMeshPro textObje;
        private int count = 60;
        private Coroutine counterCoroutine;

        private void Awake()
        {
            Instance = this;
            anim = GetComponent<Animator>();
            textObje = this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        }

        private void Start()
        {
            // Coroutine'u başlat
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

            if (count <= 0)
            {
                count = 60;
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

        private void OnEnable()
        {
            counterCoroutine = StartCoroutine(StartCounter());
        }

        private void OnDisable()
        {
            // Script yok edildiğinde Coroutine'u durdur
            StopCounter();
        }
    }
}