namespace Game.Pull
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;

    public class PullVisualizer : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Init()
        {

        }

        private void OnEnable()
        {
            EventManager.onSwipeUp += PullAnimActiveTrue;
            EventManager.onSwipeDown += PullAnimPassiveTrue;
        }

        private void OnDisable()
        {
            EventManager.onSwipeUp -= PullAnimActiveTrue;
            EventManager.onSwipeDown -= PullAnimPassiveTrue;
        }

        private void PullAnimActiveTrue()
        {
            animator.SetBool("active", true);
        }
        private void PullAnimActiveFalse()
        {
            animator.SetBool("active", false);
        }
        private void PullAnimPassiveTrue()
        {
            animator.SetBool("passive", true);
        }
        private void PullAnimPassiveFalse()
        {
            animator.SetBool("passive", false);
        }
    }
}
