using UnityEngine;
using Game.Card;
using Game.Pump;
using Game.Pull;
using Game.SOLevel;
using Game.MotivationText;
using Game.UI;
using Game.PlayerPrefs;
using Edleron.Events;
using Game.Heart;
using Game.Stars;


public enum TutorialState
{
    Inactive,
    Passive,
    Down,
    Up
}

public class GameManager : MonoBehaviour
{
    private TutorialState tutorialState = TutorialState.Inactive;
    public PlayerPrefsManager pPrefManager;

    private void Awake()
    {
        tutorialState = TutorialState.Inactive;
    }

    private void Start()
    {
        TutorialsActive();
        LevelGenerating();
        // PumpManager.Instance.StartPumping();
        PullManager.Instance.StartPulling();
        CardManager.Instance.StartCarding();
    }

    private void OnEnable()
    {
        EventManager.onEndLevel += EndLEvel;
        EventManager.onNewLevel += NewLevel;
        EventManager.onRepeatLevel += RepeatLevel;
        EventManager.onUITrigger += UITrigger;
    }

    private void OnDisable()
    {
        EventManager.onEndLevel -= EndLEvel;
        EventManager.onNewLevel -= NewLevel;
        EventManager.onRepeatLevel -= RepeatLevel;
        EventManager.onUITrigger -= UITrigger;
    }

    private void EndLEvel()
    {
        Debug.Log("End Level");
        EventManager.Fire_onSwipeLock(true);
        LevelGenerating();
        CardManager.Instance.EndCarding();
        StarVisualizer.Instance.StopCounter();
        pPrefManager.SaveLevel(LevelManager.Instance.GetLevelIndex());
    }

    private void NewLevel()
    {
        Debug.Log("New Level");
        LevelManager.Instance.SetRoundCount();
        EventManager.Fire_onSwipeLock(false);
        MTextVisualizer.Instance.SetNewLevel();
        HeardVisualizer.Instance.SetSkore(5);
        StarVisualizer.Instance.SetCount(140);
        PumpManager.Instance.StartPumping();
        CardManager.Instance.NewCard();
    }

    private void RepeatLevel()
    {
        Debug.Log("Repeat Level");
        LevelManager.Instance.SetRoundCount();
        EventManager.Fire_onSwipeLock(false);
        MTextVisualizer.Instance.SetRepeatLevel();
        HeardVisualizer.Instance.SetSkore(5);
        StarVisualizer.Instance.SetCount(140);
        PumpManager.Instance.StartPumping();
        CardManager.Instance.NewCard();
    }

    private void LevelGenerating()
    {
        int levelIndex = LevelManager.Instance.GetLevelIndex();
        if (levelIndex > 10)
        {
            LevelManager.Instance.LevelGenerate();
        }
    }

    private void UITrigger(string value)
    {
        switch (value)
        {
            case "CloseTutorialDownPanel":
                TutorialsDownPassive();
                break;
            case "CloseTutorialUpPanel":
                TutorialsUpPassive();
                break;
        }
    }

    private void TutorialsActive()
    {
        bool triggerTutorials = LevelManager.Instance.GetTutorialLevel();
        if (triggerTutorials)
        {
            EventManager.Fire_onSwipeLock(true);
            UIVisualizer.Instance.locked = false;
            Invoke("TutorialsDownActive", 2f);
        }
        else
        {
            UIVisualizer.Instance.locked = true;
        }
    }

    // Invoke
    private void TutorialsDownActive()
    {
        bool level = LevelManager.Instance.GetTutorialLevel();
        if (level && tutorialState == TutorialState.Inactive)
        {
            tutorialState = TutorialState.Down;
            CardManager.Instance.PauseGame();
            UIVisualizer.Instance.DownTutorials();
        }
    }

    private void TutorialsDownPassive()
    {
        if (tutorialState == TutorialState.Down)
        {
            tutorialState = TutorialState.Inactive;
            CardManager.Instance.ResumeGame();
            Invoke("TutorialsUpActive", 2.0f);
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
            UIVisualizer.Instance.UpTutorials();
        }
    }

    private void TutorialsUpPassive()
    {
        if (tutorialState == TutorialState.Up)
        {
            tutorialState = TutorialState.Passive;
            CardManager.Instance.ResumeGame();
            LevelManager.Instance.SetTutorialLevel();
            UIVisualizer.Instance.locked = true;
            EventManager.Fire_onSwipeLock(false);
        }
    }
}