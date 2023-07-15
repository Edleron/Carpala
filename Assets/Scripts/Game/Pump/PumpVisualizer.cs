namespace Game.Pump
{
    using UnityEngine;

    public class PumpVisualizer : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PumpAnimActiveTrue()
        {
            animator.SetBool("active", true);
        }
        private void PumpAnimActiveFalse()
        {
            animator.SetBool("active", false);
        }
        public void PumpAnimPassiveTrue()
        {
            animator.SetBool("passive", true);
        }
        private void PumpAnimPassiveFalse()
        {
            animator.SetBool("passive", false);
            PumpAnimActiveTrue();
        }
    }
}
