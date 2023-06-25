namespace Game.Field
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;

    public class FieldDetect : MonoBehaviour
    {
        public Transform target;
        private bool IsInCheckDetect = false;
        private Vector2 startPosition;

        private void Awake()
        {
            IsInCheckDetect = false;
            Constants.IsInCheckSwipe = false;
            startPosition = transform.position;
        }

        private void OnEnable()
        {
            EventManager.onSwipeUp += LaunchControl;
            transform.position = startPosition;
        }

        private void OnDisable()
        {
            EventManager.onSwipeUp -= LaunchControl;
            transform.position = startPosition;
        }

        private void Update()
        {
            if (IsInCheckDetect && Constants.IsInCheckSwipe)
            {
                LaunchObject();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constants.Detect))
            {
                IsInCheckDetect = true;
            }

            if (other.CompareTag(Constants.Target))
            {
                IsInCheckDetect = false;
                Constants.IsInCheckSwipe = false;

                transform.position = startPosition;
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Constants.Detect))
            {
                if (!Constants.IsInCheckSwipe)
                {
                    IsInCheckDetect = false;
                }
            }
        }

        private void LaunchObject()
        {
            Vector2 currentPosition = transform.position;
            transform.position = Vector2.MoveTowards(currentPosition, target.position, 10.0f * Time.deltaTime);
        }

        private void LaunchControl()
        {
            if (IsInCheckDetect)
            {
                Constants.IsInCheckSwipe = true;
            }
        }
    }
}