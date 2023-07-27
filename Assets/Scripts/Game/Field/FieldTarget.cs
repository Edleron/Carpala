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
                LevelManager.Instance.CheckResult(int.Parse(textObje.text));
                // TODO -> Lock False Edilmesi Gerekiyor, Ama Oyun Hızlı Olduğu İçin Patlıyor.
                // EventManager.Fire_onSwipeLock(false);
            }
        }
    }
}