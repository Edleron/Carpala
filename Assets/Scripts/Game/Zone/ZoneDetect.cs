namespace Game.Zone
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;

    public class ZoneDetect : MonoBehaviour
    {
        public Transform target; // Hedef noktanın referansı
        private bool checkDetect = false;
        private bool checkSwipe = false;

        private void Awake()
        {
            checkDetect = false;
            checkSwipe = false;
        }

        private void OnEnable()
        {
            EventManager.onSwipeUp += LaunchControl;
        }

        private void OnDisable()
        {
            EventManager.onSwipeUp -= LaunchControl;
        }

        private void Update()
        {
            if (checkDetect && checkSwipe)
            {
                LaunchObject();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(ZoneTags.Detect))
            {
                checkDetect = true;
                Debug.Log(gameObject.name);
            }
        }

        private void LaunchObject()
        {
            Vector2 currentPosition = transform.position;
            transform.position = Vector2.MoveTowards(currentPosition, target.position, 10.0f * Time.deltaTime);
        }

        private void LaunchControl()
        {
            checkSwipe = true;
        }
    }
}

