namespace Game.Field
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;

    public enum FieldDetectState
    {
        Inactive,
        Active,
        Moving
    }

    public class FieldDetect : MonoBehaviour
    {
        public Transform target;
        private FieldDetectState state = FieldDetectState.Inactive;
        [HideInInspector] public Vector2 startPosition;

        private void Awake()
        {
            state = FieldDetectState.Inactive;
            startPosition = transform.position;
        }

        private void OnEnable()
        {
            transform.position = startPosition;
        }

        private void OnDisable()
        {
            transform.position = startPosition;
        }

        private void Update()
        {
            if (state == FieldDetectState.Moving)
            {
                LaunchObject();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constants.Detect))
            {
                state = FieldDetectState.Active;
            }

            if (other.CompareTag(Constants.Target))
            {
                state = FieldDetectState.Inactive;
                transform.position = startPosition;
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Constants.Detect))
            {
                if (state != FieldDetectState.Moving)
                {
                    state = FieldDetectState.Inactive;
                }
            }
        }

        private void LaunchObject()
        {
            Vector2 currentPosition = transform.position;
            transform.position = Vector2.MoveTowards(currentPosition, target.position, 5.0f * Time.deltaTime);
        }

        public void LaunchControl()
        {
            if (state == FieldDetectState.Active)
            {
                state = FieldDetectState.Moving;
            }
        }
    }
}