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
            EventManager.onSwipeUp += PumpAnimActiveTrue;
            EventManager.onSwipeDown += PumpAnimPassiveTrue;
        }

        private void OnDisable()
        {
            EventManager.onSwipeUp -= PumpAnimActiveTrue;
            EventManager.onSwipeDown -= PumpAnimPassiveTrue;
        }

        private void PumpAnimActiveTrue()
        {
            animator.SetBool("active", true);
        }
        private void PumpAnimActiveFalse()
        {
            animator.SetBool("active", false);
        }
        private void PumpAnimPassiveTrue()
        {
            animator.SetBool("passive", true);
        }
        private void PumpAnimPassiveFalse()
        {
            animator.SetBool("passive", false);
        }
    }
}
