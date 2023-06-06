namespace Game.Pull
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;

    public class PullController : MonoBehaviour
    {
        private PullVisualizer pullVisualizer;

        private void Awake()
        {
            pullVisualizer = GetComponent<PullVisualizer>();
        }

        private void OnEnable()
        {
            EventManager.onSwipeUp += pullVisualizer.PullAnimActiveTrue;
            EventManager.onSwipeDown += pullVisualizer.PullAnimPassiveTrue;
        }

        private void OnDisable()
        {
            EventManager.onSwipeUp -= pullVisualizer.PullAnimActiveTrue;
            EventManager.onSwipeDown -= pullVisualizer.PullAnimPassiveTrue;
        }

    }
}
