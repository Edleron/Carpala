namespace Game.Pull
{
    using UnityEngine;

    public class PullVisualizer : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PullAnimActiveTrue()
        {
            animator.SetBool("active", true);
        }
        private void PullAnimActiveFalse()
        {
            animator.SetBool("active", false);
        }
        public void PullAnimPassiveTrue()
        {
            animator.SetBool("passive", true);
        }
        private void PullAnimPassiveFalse()
        {
            animator.SetBool("passive", false);
        }
    }
}
