namespace Game.Field
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;
    using Game.SOLevel;
    using TMPro;

    public class FieldTarget : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constants.Zone))
            {
                TextMeshPro textObje = other.gameObject.transform.GetChild(3).GetComponent<TextMeshPro>();
                EventManager.Fire_onResult(int.Parse(textObje.text));
            }
        }
    }
}