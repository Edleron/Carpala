using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.SOLevel;
using TMPro;

public class ZoneTarget : MonoBehaviour
{
    // Todo
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ZoneUtils.Zone))
        {
            // TODO
            TextMeshPro textObje = other.gameObject.transform.GetChild(3).GetComponent<TextMeshPro>();
            LevelManager.Instance.CheckResult(int.Parse(textObje.text));
        }
    }
}
