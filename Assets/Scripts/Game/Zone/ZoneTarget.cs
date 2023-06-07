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
    private int test = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ZoneUtils.Zone))
        {
            test++;

            // TODO
            Debug.Log(other.name);
            StartCoroutine(CorrentState());
        }
    }

    private IEnumerator CorrentState()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Test" + test.ToString());
        if (test == 4)
        {
            test = 0;
            PumpManager.Instance.StartPumping();
            PullManager.Instance.StartPulling();
            CardManager.Instance.StartCarding();
        }
        else
        {
            EventManager.Fire_onCorrectUp();
        }
    }
}
