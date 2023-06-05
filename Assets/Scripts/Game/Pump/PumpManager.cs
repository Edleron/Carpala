namespace Game.Pump
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PumpManager : MonoBehaviour
    {
        public static PumpManager Instance { get; private set; }

        private PumpController pumpController;

        private void Awake()
        {
            Instance = this;
            pumpController = GetComponent<PumpController>();
        }

        public void StartPump()
        {
            pumpController.pumpVisualizer.ActivePumping();
        }
        public void StopPump()
        {
            pumpController.pumpVisualizer.PassivePumping();
        }
    }
}