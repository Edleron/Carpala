namespace Game.Card
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CardShake : MonoBehaviour
    {
        private float shakeDuration = 1.15f;  // Shake süresi
        private float shakeIntensity = 0.05f; // Shake yoğunluğu

        private Vector3 initialPosition;
        private float currentShakeDuration;

        private void Start()
        {
            initialPosition = transform.position;
        }

        private void Update()
        {
            if (currentShakeDuration > 0)
            {
                // Shake efekti uygula
                transform.position = initialPosition + Random.insideUnitSphere * shakeIntensity;

                currentShakeDuration -= Time.deltaTime;
            }
            else
            {
                // Shake süresi dolunca objeyi başlangıç konumuna getir
                transform.position = initialPosition;
            }
        }

        public void StartShake()
        {
            currentShakeDuration = shakeDuration;
        }
    }
}
