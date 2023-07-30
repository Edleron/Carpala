namespace Game.Speed
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Edleron.Events;
    using Game.Stars;

    public class SpeedVisualizer : MonoBehaviour
    {
        public static SpeedVisualizer Instance { get; private set; }
        private Slider slider;
        private float previousSliderValue = 0;
        private bool locked = false;

        private void Awake()
        {
            Instance = this;
            slider = GetComponent<Slider>();
        }

        public void SetSliderValue(int value)
        {
            slider.minValue = value / 2;
            slider.maxValue = value * 2;

            slider.value = (slider.maxValue + slider.minValue) / 2;

            previousSliderValue = slider.value;
            locked = true;
        }

        public void OnChangedValue()
        {
            if (locked)
            {
                float currentSliderValue = slider.value;
                float difference = Mathf.Abs(currentSliderValue - previousSliderValue);

                if (difference >= 5f)
                {
                    StarVisualizer.Instance.ScoreTimerUpdated();
                }
                previousSliderValue = currentSliderValue;

                // Set Rotate
                EventManager.Fire_onUISlider((int)slider.value);
            }
        }

        public void CountSliderValue()
        {
            slider.value += 1;
        }

    }
}