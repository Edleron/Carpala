namespace Game.Audio
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AudioManager : MonoBehaviour
    {

        [Header("Swipe")]
        [SerializeField] AudioClip swipeClip;
        [SerializeField][Range(0f, 1f)] float swipeVolume = 1.0f;


        [Header("CorrectShoting")]
        [SerializeField] AudioClip CorrectShootingClip;
        [SerializeField][Range(0f, 1f)] float CorrectShootingVolume = 1.0f;


        [Header("WrongShoting")]
        [SerializeField] AudioClip WrongShootingClip;
        [SerializeField][Range(0f, 1f)] float WrongShootingVolume = 1.0f;




        public static AudioManager Instance { get; private set; }


        private void Awake()
        {
            Instance = this;
        }

        private void ManageSingleton()
        {
            // int instanceCount = FindObjectsOfType(GetType()).Length;
            // if (instanceCount > 1)
            if (Instance != null)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void PlayCorrectShootingClip()
        {
            PlayClip(CorrectShootingClip, CorrectShootingVolume);
        }

        public void PlayWrongShootingClip()
        {
            PlayClip(WrongShootingClip, WrongShootingVolume);
        }

        public void PlaySwipeClip()
        {
            PlayClip(swipeClip, swipeVolume);
        }


        private void PlayClip(AudioClip clip, float volume)
        {
            if (clip != null)
            {
                Vector3 camPos = Camera.main.transform.position;
                AudioSource.PlayClipAtPoint(clip, camPos, volume);
            }
        }
    }
}