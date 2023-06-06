namespace Game.Pump
{
    using System.Collections;
    using System.Collections.Generic;
    using Edleron.Events;
    using UnityEngine;

    public class PumpVisualizer : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            EventManager.onSwipeUp += ActivePumping;
        }

        private void OnDisable()
        {
            EventManager.onSwipeUp -= ActivePumping;
        }

        public void ActivePumping()
        {
            animator.SetBool("pumping", true);
        }

        public void PassivePumping()
        {
            animator.SetBool("pumping", false);
        }
    }
}
