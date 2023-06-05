namespace Game.Card
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

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
    }
}