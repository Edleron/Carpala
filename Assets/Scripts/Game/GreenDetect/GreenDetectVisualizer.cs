namespace GreenDetect
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GreenDetectVisualizer : MonoBehaviour
    {
        private SpriteRenderer rend;

        private Coroutine colorChangeCoroutine; // Renk değişimi Coroutine referansı

        private void Awake()
        {
            rend = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constants.Zone))
            {
                if (colorChangeCoroutine != null)
                {
                    // Eğer bir renk değişimi Coroutine'i çalışıyorsa, durdurun
                    StopCoroutine(colorChangeCoroutine);
                }

                // Renk değişimi Coroutine'ini başlatın
                colorChangeCoroutine = StartCoroutine(ChangeColorCoroutine(Color.white, 0.5f));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Constants.Zone))
            {
                if (colorChangeCoroutine != null)
                {
                    // Eğer bir renk değişimi Coroutine'i çalışıyorsa, durdurun
                    StopCoroutine(colorChangeCoroutine);
                }

                // Renk değişimi Coroutine'ini başlatın
                colorChangeCoroutine = StartCoroutine(ChangeColorCoroutine(Color.red, 0.5f));
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
    }

}