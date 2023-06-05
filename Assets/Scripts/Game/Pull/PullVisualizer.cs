namespace Game.Pull
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PullVisualizer : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Init()
        {
            animator.SetBool("visible", true);
        }
    }
}
