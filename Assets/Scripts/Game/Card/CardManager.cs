namespace Game.Card
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CardManager : MonoBehaviour
    {
        public static CardManager Instance { get; private set; }

        private CardController cardController;

        private void Awake()
        {
            Instance = this;
            cardController = GetComponent<CardController>();
        }
    }
}
