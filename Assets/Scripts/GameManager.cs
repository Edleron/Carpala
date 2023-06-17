using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Card;
using Game.Pump;
using Game.Pull;

public class GameManager : MonoBehaviour
{
    // Todo -> Swipe'lar yapıldığında, baykus ve pull aynı anda hareket etmemelidir.
    // -> Düş yeri,
    // -> Normal oyunlar kaybettiriyor -£eüitsel oyunlar kaybetmek yok ! daha kolay kazanma.
    // Bu sebeple ara ekran tasarlanakcak !

    // Tüm kombinasyonlar -> 4 * 6, 3 * 8 - 2 Yapıalcak !
    // çocuklar, parmakları küçük olduğu için parmak boyutuna dikkat et

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