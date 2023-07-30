namespace Game.Pull
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;
    using Game.SOLevel;
    using TMPro;

    public class PullActuator : MonoBehaviour
    {
        public static PullActuator Instance { get; private set; }
        private Animator animator;
        private SpriteRenderer sp;
        private Transform tForm;

        private void Awake()
        {
            Instance = this;
            animator = GetComponent<Animator>();
            sp = GetComponent<SpriteRenderer>();
            tForm = GetComponent<Transform>();

        }

        private void Start()
        {
            InitObject();
        }

        private void InitObject()
        {
            sp.enabled = false;
            animator.enabled = false;
            tForm.localPosition = new Vector3(0f, 20.0f, 0f);
            tForm.localScale = new Vector3(0f, 0f, 1f);
        }

        public void BeginPulling()
        {
            if (!sp.enabled)
            {
                sp.enabled = true;
            }

            if (!animator.enabled)
            {
                animator.enabled = true;
            }
            PullAnimActiveTrue();
        }
        public void RunPulling()
        {
            PullAnimPassiveTrue();
        }


        private void PullGenerate()
        {
            var arr = LevelManager.Instance.GetPullValue();

            for (var i = 0; i < arr.Length; i++)
            {
                Transform transformObje = this.gameObject.transform.GetChild(i);
                TextMeshPro textObje = transformObje.GetComponent<TextMeshPro>();
                textObje.text = arr[i].ToString();
            }
        }


        #region HELPER
        // ANIMASYON'DA TETIKLENİR
        private void PullAnimActiveTrue()
        {
            animator.SetBool("active", true);
            PullGenerate();
        }
        private void PullAnimActiveFalse()
        {
            EventManager.Fire_onSwipeLock(false);
            animator.SetBool("active", false);
        }

        // ANIMASYON'DA TETIKLENİR
        private void PullAnimPassiveTrue()
        {
            animator.SetBool("passive", true);
        }
        private void PullAnimPassiveFalse()
        {
            animator.SetBool("passive", false);
            PullAnimActiveTrue();
        }

        // SWIPE DOWN STATE || PullAnimPassiveTrue -> PullAnimPassiveFalse -> PullAnimActiveTrue -> PullAnimActiveFalse
        #endregion
    }
}