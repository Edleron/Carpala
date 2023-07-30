namespace Game.GreenDetect
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GreenDetectVisualizer : MonoBehaviour
    {
        public static GreenDetectVisualizer Instance { get; private set; }

        private SpriteRenderer rend;
        private Coroutine colorChangeCoroutine; // Renk değişimi Coroutine referansı
        private string DetectName = "None";

        private void Awake()
        {
            Instance = this;
            rend = GetComponent<SpriteRenderer>();
            DetectName = "None";
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constants.Zone))
            {
                DetectName = other.name;
                if (colorChangeCoroutine != null)
                {
                    // Eğer bir renk değişimi Coroutine'i çalışıyorsa, durdurun
                    StopCoroutine(colorChangeCoroutine);
                }

                // Renk değişimi Coroutine'ini başlatın
                colorChangeCoroutine = StartCoroutine(ChangeColorCoroutine(Color.white, 0.15f));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Constants.Zone))
            {
                DetectName = "None";
                if (colorChangeCoroutine != null)
                {
                    // Eğer bir renk değişimi Coroutine'i çalışıyorsa, durdurun
                    StopCoroutine(colorChangeCoroutine);
                }

                // Renk değişimi Coroutine'ini başlatın
                colorChangeCoroutine = StartCoroutine(ChangeColorCoroutine(Color.red, 0.15f));
            }
        }

        private IEnumerator ChangeColorCoroutine(Color targetColor, float duration)
        {
            Color startColor = rend.color;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                rend.color = Color.Lerp(startColor, targetColor, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rend.color = targetColor; // Hedef rengi kesin olarak ayarla

            // Renk değişimi Coroutine'i tamamlandıktan sonra Coroutine referansını temizleyin
            colorChangeCoroutine = null;
        }

        public string GetDetectName()
        {
            return DetectName;
        }
    }

}