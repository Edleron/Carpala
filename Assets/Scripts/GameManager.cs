using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Card;
using Game.Pump;
using Game.Pull;
using Game.SOLevel;
using Game.Panel;
using Game.Stars;
using Game.Rhythmic;
using Edleron.Events;

public enum TutorialState
{
    Inactive,
    Passive,
    Down,
    Up
}

public class GameManager : MonoBehaviour
{
    // Todo -> Swipe'lar yapıldığında, baykus ve pull aynı anda hareket etmemelidir.
    // -> Düş yeri,
    // -> Normal oyunlar kaybettiriyor -£eüitsel oyunlar kaybetmek yok ! daha kolay kazanma.
    // Bu sebeple ara ekran tasarlanakcak !

    // Tüm kombinasyonlar -> 4 * 6, 3 * 8 - 2 Yapıalcak !
    // çocuklar, parmakları küçük olduğu için parmak boyutuna dikkat et
    private TutorialState tutorialState = TutorialState.Inactive;

    private void Awake()
    {
        tutorialState = TutorialState.Inactive;
    }

    private void Start()
    {
        TutorialsActive();
        PumpManager.Instance.StartPumping();
        PullManager.Instance.StartPulling();
        CardManager.Instance.StartCarding();
        Invoke("TutorialsDownActive", 3f);
    }

    private void OnEnable()
    {

        EventManager.onCheckLevel += EndLevel;
    }

    private void OnDisable()
    {
        EventManager.onCheckLevel -= EndLevel;
    }

    private void EndLevel()
    {
        int stamp = LevelManager.Instance.GetStampCount();
        int round = LevelManager.Instance.GetRoundCount();

        // Debug.Log(stamp + " - " + round); // TODO

        if (stamp == round)
        {
            CardManager.Instance.EndCarding();
            LevelManager.Instance.SetRoundCount();
            Invoke("NewLevel", 0.75f);
        }
    }

    private void NewLevel()
    {
        bool newLevelControl = RhytmicManager.Instance.GetRhytmic();

        if (newLevelControl)
        {
            Debug.Log("Level Yeni Level");
            LevelManager.Instance.SetLevelIndex();
            int levelIndex = LevelManager.Instance.GetLevelIndex();            
            RhytmicManager.Instance.SetDict(levelIndex);            
            PumpManager.Instance.StartPumping();
            CardManager.Instance.StartCarding();
        }
        else
        {          
            Debug.Log("Level Tekrarı");
            // Level Tekranı
            PumpManager.Instance.StartPumping();
            // PullManager.Instance.StartPulling();
            CardManager.Instance.StartCarding();
        }        
    }

    private void TutorialsActive()
    {
        EventManager.Fire_onSwipeLock(true);
    }

    // Invoke
    private void TutorialsDownActive()
    {
        bool level = LevelManager.Instance.GetTutorialLevel();
        if (level && tutorialState == TutorialState.Inactive)
        {
            PanelChange.Instance.locked = true;

            tutorialState = TutorialState.Down;
            CardManager.Instance.PauseGame();
            StarVisualizer.Instance.StopCounter();
            PanelTutorials.Instance.DownTutorials(true);
            EventManager.onSwipeDown += TutorialsDownPassive;
            EventManager.Fire_onSwipeLock(false);
        }
    }

    private void TutorialsDownPassive()
    {
        if (tutorialState == TutorialState.Down)
        {
            tutorialState = TutorialState.Inactive;
            CardManager.Instance.ResumeGame();
            StarVisualizer.Instance.StartingCounter();
            PanelTutorials.Instance.DownTutorials(false);
            EventManager.onSwipeDown -= TutorialsDownPassive;
            EventManager.Fire_onSwipeLock(true);
            Invoke("TutorialsUpActive", 3.0f);
        }
    }

    // Invoke
    private void TutorialsUpActive()
    {
        bool level = LevelManager.Instance.GetTutorialLevel();
        if (level && tutorialState == TutorialState.Inactive)
        {
            tutorialState = TutorialState.Up;
            CardManager.Instance.PauseGame();
            StarVisualizer.Instance.StopCounter();
            PanelTutorials.Instance.UpTutorials(true);
            EventManager.onSwipeUp += TutorialsUpPassive;
            EventManager.Fire_onSwipeLock(false);
        }
    }

    private void TutorialsUpPassive()
    {
        if (tutorialState == TutorialState.Up)
        {
            tutorialState = TutorialState.Passive;
            CardManager.Instance.ResumeGame();
            PanelTutorials.Instance.UpTutorials(false);
            StarVisualizer.Instance.StartingCounter();
            EventManager.onSwipeUp -= TutorialsUpPassive;

            LevelManager.Instance.SetTutorialLevel();
            PanelChange.Instance.locked = false;
        }
    }
}