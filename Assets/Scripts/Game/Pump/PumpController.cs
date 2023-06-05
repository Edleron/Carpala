namespace Game.Pump
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PumpController : MonoBehaviour
    {
        [HideInInspector]
        public PumpVisualizer pumpVisualizer;

        private void Awake()
        {
            pumpVisualizer = GetComponent<PumpVisualizer>();
        }
    }
}
