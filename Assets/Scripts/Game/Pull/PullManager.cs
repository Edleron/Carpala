namespace Game.Pull
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PullManager : MonoBehaviour
    {
        public static PullManager Instance { get; private set; }
        private PullController pullController;

        private void Awake()
        {
            Instance = this;
            pullController = GetComponent<PullController>();
        }

        public void StartPulling()
        {
            pullController.pullVisualizer.PullAnimActiveTrue();
        }
    }
}