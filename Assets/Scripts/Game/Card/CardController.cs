namespace Game.Card
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Game.Pump;
    // using Edleron.Events;

    public class CardController : MonoBehaviour
    {
        private CardVisualizer cardVisualizer;

        private void Awake()
        {
            cardVisualizer = GetComponent<CardVisualizer>();
        }

        public void StartCard()
        {
            cardVisualizer.Shake();
            cardVisualizer.CardAnimActiveTrue();
            cardVisualizer.StampFalse();
            cardVisualizer.SetRotationSpeed();
            cardVisualizer.SetRotationReset();
            StartCoroutine(cardVisualizer.CardGenerate(1));
            StartCoroutine(cardVisualizer.CardRotate(0.95f, true));
        }

        public void EndCard()
        {
            cardVisualizer.Shake();
            cardVisualizer.CardAnimPassiveTrue();
            cardVisualizer.rotationControl = false;
        }

        public void PausedGame()
        {
            cardVisualizer.PauseGame();
        }

        public void ResumedGame()
        {
            cardVisualizer.ResumeGame();
        }

        private void OnEnable()
        {
            // TODO
            // EventManager.onSwipeUp += cardVisualizer.CardAnimActiveTrue;
            // EventManager.onSwipeDown += cardVisualizer.CardAnimPassiveTrue;
        }

        private void OnDisable()
        {
            // TODO
            // EventManager.onSwipeUp -= cardVisualizer.CardAnimActiveTrue;
            // EventManager.onSwipeDown -= cardVisualizer.CardAnimPassiveTrue;
        }
    }
}