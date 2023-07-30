namespace Game.Pump
{
    using UnityEngine;

    public class PumpActuator : MonoBehaviour
    {
        public static PumpActuator Instance { get; private set; }
        private Animator animator;

        private void Awake()
        {
            Instance = this;
            animator = GetComponent<Animator>();
        }

        public void BeginPumping()
        {
            PumpAnimActiveTrue();
        }

        public void RunPumping()
        {
            PumpAnimPassiveTrue();
        }





        #region HELPER
        private void PumpAnimActiveTrue()
        {
            animator.SetBool("active", true);
        }
        // ANIMASYON'DA TETIKLENİR
        private void PumpAnimActiveFalse()
        {
            animator.SetBool("active", false);
        }


        private void PumpAnimPassiveTrue()
        {
            animator.SetBool("passive", true);
        }
        // ANIMASYON'DA TETIKLENİR
        private void PumpAnimPassiveFalse()
        {
            animator.SetBool("passive", false);
            PumpAnimActiveTrue();
        }

        // SWIPE DOWN STATE || PumpAnimPassiveTrue -> PumpAnimPassiveFalse -> PumpAnimActiveTrue -> PumpAnimActiveFalse
        #endregion


    }
}