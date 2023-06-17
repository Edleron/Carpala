namespace Game.Emoji
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;

    public class WrongEmojiVisualizer : MonoBehaviour
    {
        // Bu script, Sahnede Correct ve Wrong içerisimnde bulunan 6 adet particle sistemde tek script olarak kullaılmıştır.
        private ParticleSystem particleSystem;

        private void Awake()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            EventManager.onWrong += PlayWrongFx;
        }

        private void OnDisable()
        {
            EventManager.onWrong -= PlayWrongFx;
        }

        private void PlayWrongFx()
        {
            particleSystem.Play();
        }
    }
}
