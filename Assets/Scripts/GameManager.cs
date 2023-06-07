using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Card;
using Game.Pump;
using Game.Pull;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake");
    }
    private void Start()
    {
        Debug.Log("Start");
        PumpManager.Instance.StartPumping();
        PullManager.Instance.StartPulling();
        CardManager.Instance.StartCarding();
    }
}