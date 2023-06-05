namespace Game.Pump
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PumpVisualizer : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
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
