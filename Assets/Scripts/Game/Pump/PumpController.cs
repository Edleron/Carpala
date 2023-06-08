namespace Game.Pump
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;

    public class PumpController : MonoBehaviour
    {
        [HideInInspector] public PumpVisualizer pumpVisualizer;

        private void Awake()
        {
            pumpVisualizer = GetComponent<PumpVisualizer>();
        }

        private void OnEnable()
        {
            EventManager.onSwipeDown += pumpVisualizer.PumpAnimPassiveTrue;
        }

        private void OnDisable()
        {
            EventManager.onSwipeDown -= pumpVisualizer.PumpAnimPassiveTrue;
        }
    }
}
