namespace Game.Pull
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PullController : MonoBehaviour
    {
        private PullVisualizer pullVisualizer;

        private void Awake()
        {
            pullVisualizer = GetComponent<PullVisualizer>();
        }

        private void Start()
        {
            pullVisualizer.Init();
        }
    }
}
