namespace Game.Emoji
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;

    public class ExpEmojiVisualizer : MonoBehaviour
    {
        // Bu script, Sahnede Correct ve Wrong içerisimnde bulunan 6 adet particle sistemde tek script olarak kullaılmıştır.
        private ParticleSystem particleSystem;

        private void Awake()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            EventManager.onExplosion += PlayExptFx;
        }

        private void OnDisable()
        {
            EventManager.onExplosion -= PlayExptFx;
        }

        private void PlayExptFx()
        {
            particleSystem.Play();
        }
    }
}