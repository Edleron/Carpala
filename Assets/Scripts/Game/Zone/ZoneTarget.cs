using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edleron.Events;
using Game.Card;
using Game.Pump;
using Game.Pull;

public class ZoneTarget : MonoBehaviour
{
    // Todo
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ZoneUtils.Zone))
        {
            // TODO
            Debug.Log(other.name);
        }
    }
}
