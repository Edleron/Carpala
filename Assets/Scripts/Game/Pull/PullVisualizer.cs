namespace Game.Pull
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Game.SOLevel;
    using TMPro;

    public class PullVisualizer : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PullInit()
        {
            // TODO -> Like a Card, 
        }

        public void PullGenerate()
        {
            var arr = LevelManager.Instance.GetPullValue();

            for (var i = 0; i < arr.Length; i++)
            {
                Transform transformObje = this.gameObject.transform.GetChild(i);
                TextMeshPro textObje = transformObje.GetComponent<TextMeshPro>();
                textObje.text = arr[i].ToString();
            }
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
            PullGenerate();
            PullAnimActiveTrue();
        }
    }
}
