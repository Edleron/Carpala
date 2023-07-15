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
using Game.MotivationText;
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
        EventManager.onEndLevel += EndLEvel;
        EventManager.onNewLevel += NewLevel;
        EventManager.onRepeatLevel += RepeatLevel;
    }

    private void OnDisable()
    {
        EventManager.onEndLevel -= EndLEvel;
        EventManager.onNewLevel -= NewLevel;
        EventManager.onRepeatLevel -= RepeatLevel;
    }

    private void EndLEvel()
    {
        Debug.Log("End Level");
        CardManager.Instance.EndCarding();
    }

    private void NewLevel()
    {
        Debug.Log("New Level");
        MTextVisualizer.Instance.SetNewLevel();
        int levelIndex = LevelManager.Instance.GetLevelIndex();
        RhytmicManager.Instance.SetDict(levelIndex);
        PumpManager.Instance.StartPumping();
        CardManager.Instance.StartCarding();
    }

    private void RepeatLevel()
    {
        Debug.Log("Repeat Level");
        PumpManager.Instance.StartPumping();
        MTextVisualizer.Instance.SetRepeatLevel();
        CardManager.Instance.StartCarding();
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