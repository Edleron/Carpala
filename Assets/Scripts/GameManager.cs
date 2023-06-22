using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Card;
using Game.Pump;
using Game.Pull;
using Game.SOLevel;
using Edleron.Events;

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
    }

    private void OnEnable()
    {
        EventManager.onCheckLevel += EndLevel;
    }

    private void OnDisable()
    {
        EventManager.onCheckLevel -= EndLevel;
    }

    private void Start()
    {
        PumpManager.Instance.StartPumping();
        PullManager.Instance.StartPulling();
        CardManager.Instance.StartCarding();
    }

    private void EndLevel()
    {
        int stamp = LevelManager.Instance.GetStampCount();
        int round = LevelManager.Instance.GetRoundCount();

        Debug.Log(stamp + " - " + round);

        if (stamp == round)
        {
            CardManager.Instance.EndCarding();
            LevelManager.Instance.SetRoundCount();
            Invoke("NewLevel", 0.75f);
        }
    }

    private void NewLevel()
    {
        PumpManager.Instance.StartPumping();
        // PullManager.Instance.StartPulling();
        CardManager.Instance.StartCarding();
    }
}