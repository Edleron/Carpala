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

        public void InitialCard()
        {
            cardVisualizer.Shake();
            cardVisualizer.CardInit();
            cardVisualizer.SetRotationSpeed();
            StartCoroutine(cardVisualizer.CardGenerate(1));
            StartCoroutine(cardVisualizer.CardRotate(0.95f, true));
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