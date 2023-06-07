using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTarget : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Null"))
        {

        }

        // TODO
        Debug.Log(other.name);
    }
}
