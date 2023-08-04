namespace Edleron.Input
{

    using System.Collections;
    using Edleron.Events;
    using Game.Audio;
    using UnityEngine;

    public class SwipeDetection : MonoBehaviour
    {
        [SerializeField] private float mininumDistance = .2f;
        [SerializeField] private float maximumTime = 1f;
        [SerializeField, Range(0f, 1f)] private float directionThreshold = .5f;

        private InputManager inputManager;

        private Vector2 startPosition;
        private float startTime;
        private Vector2 endPosition;
        private float endTime;
        private bool locked = false; // Swipe algılandı mı ?

        private void Awake()
        {
            inputManager = InputManager.Instance;
        }

        private void OnEnable()
        {
            inputManager.OnStartTouch += SwipeStart;
            inputManager.OnEndTouch += SwipeEnd;
            inputManager.OnPressTouch += Presseed;
            EventManager.onSwipeLock += SwipeLock;
        }

        private void OnDisable()
        {
            inputManager.OnStartTouch -= SwipeStart;
            inputManager.OnEndTouch -= SwipeEnd;
            inputManager.OnPressTouch -= Presseed;
            EventManager.onSwipeLock -= SwipeLock;
        }

        private void Presseed(Vector2 position)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 10f);

            if (hit.collider != null)
            {
                if (hit.transform.TryGetComponent<Collider2D>(out Collider2D ts))
                {
                    EventManager.Fire_onUITrigger(ts.name);

                    Debug.Log(ts.name);
                }
            }
        }

        private void SwipeStart(Vector2 position, float time)
        {
            startPosition = position;
            startTime = time;
        }

        private void SwipeEnd(Vector2 position, float time)
        {
            endPosition = position;
            endTime = time;
            DetectSwipe();
        }

        private void SwipeLock(bool value)
        {
            locked = value;
        }

        private void DetectSwipe()
        {
            if (!locked)
            {
                if (Vector3.Distance(startPosition, endPosition) >= mininumDistance && (endTime - startTime) <= maximumTime)
                {
                    // Debug.Log("Swipe Detected");
                    AudioManager.Instance.PlaySwipeClip();
                    Debug.DrawLine(startPosition, endPosition, Color.red, 5f);

                    Vector3 direction = endPosition - startPosition;
                    Vector2 direction2D = new Vector3(direction.x, direction.y).normalized;
                    SwipeDirection(direction2D);
                }
            }
        }

        private void SwipeDirection(Vector2 direction)
        {
            if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
            {
                // Debug.Log("Swipe Up");
                EventManager.Fire_onSwipeUp();
            }
            else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
            {
                // Debug.Log("Swipe Down");
                EventManager.Fire_onSwipeDown();
            }
            else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
            {
                // Debug.Log("Swipe Left");
            }
            else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
            {
                // Debug.Log("Swipe Right");
            }
            else
            {
                // Debug.Log("Swipe Error");
            }
        }
    }
}