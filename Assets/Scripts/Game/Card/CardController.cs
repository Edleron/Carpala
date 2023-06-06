namespace Game.Card
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;

    public class CardController : MonoBehaviour
    {
        private CardVisualizer cardVisualizer;

        private void Awake()
        {
            cardVisualizer = GetComponent<CardVisualizer>();
        }

        private void Start()
        {
            cardVisualizer.Init();
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