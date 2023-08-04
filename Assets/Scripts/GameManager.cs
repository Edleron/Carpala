using UnityEngine;
using Game.Card;
using Game.Pump;
using Game.Pull;
using Game.SOLevel;
using Game.MotivationText;
using Game.UI;
using Game.PlayerPrefs;
using Game.GreenDetect;
using Game.Speed;
using Game.Heart;
using Game.Stars;
using Game.Audio;
using Edleron.Events;
using Game.Rhythmic;
using Game.Popup;

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
    private bool finishTimeActive = false;
    private int correctCont = 0;
    private int wrondCont = 0;

    // TUTORIALS ANIMASYON EKLEME
    // INFORMATİON TEXT
    // SOUND BUTTON
    // ROZET

    private void Awake()
    {
        tutorialState = TutorialState.Inactive;
    }

    private void Start()
    {
        // HeardVisualizer.Instance.SetSkore(3);
        // StarVisualizer.Instance.SetCount(60);
        // LevelManager.Instance.SetLevelIndex(2);

        correctCont = 0;
        wrondCont = 0;

        EventManager.Fire_onSwipeLock(true);
        Invoke("StartGame", 2f);
    }

    private void StartGame()
    {
        // TUTORIALS SCENE AKTİVE
        int levelIndex = LevelManager.Instance.GetLevelIndex();
        if (levelIndex == 0)
        {
            CriticalGame();
            Invoke("TutorailsActive", 1.0f);
        }
        else
        {
            CriticalGame();
        }
    }

    private void TutorailsActive()
    {
        GamePaused();
        UIVisualizer.Instance.onClick_Clue();
        UIVisualizer.Instance.SetUILocked(false);
    }

    private void CriticalGame()
    {
        PumpActuator.Instance.BeginPumping();
        PullActuator.Instance.BeginPulling();
        CardActuator.Instance.BeginCarding();

        StarVisualizer.Instance.SetCount(60);

        int rotationSpeed = LevelManager.Instance.GetRotationSpeed();
        CardActuator.Instance.SetSpeed(rotationSpeed);
        SpeedVisualizer.Instance.SetSliderValue((int)rotationSpeed);

        StarVisualizer.Instance.CounterLauncher();
    }

    private void OnEnable()
    {
        EventManager.onFinishedTime += FinishedTime;

        EventManager.onSwipeDown += TriggerSwipeDown;
        EventManager.onSwipeUp += TriggerSwipeUp;
        EventManager.onResult += OnResult;

        EventManager.onUITrigger += UITrigger;
    }

    private void OnDisable()
    {
        EventManager.onFinishedTime -= FinishedTime;

        EventManager.onSwipeDown -= TriggerSwipeDown;
        EventManager.onSwipeUp -= TriggerSwipeUp;
        EventManager.onResult -= OnResult;

        EventManager.onUITrigger -= UITrigger;
    }

    private void TriggerSwipeDown()
    {
        EventManager.Fire_onSwipeLock(true); // False Edilen Yer ^^PullActuator^^^ -> PullAnimActiveFalse
        UIVisualizer.Instance.SetUILocked(false);

        PumpActuator.Instance.RunPumping();
        PullActuator.Instance.RunPulling();
    }

    private void TriggerSwipeUp()
    {
        string detect = GreenDetectVisualizer.Instance.GetDetectName();

        if (detect != "None")
        {
            EventManager.Fire_onSwipeLock(true); // False Edilen Yer ^^PullActuator^^^ -> PullAnimActiveFalse
            UIVisualizer.Instance.SetUILocked(false);

            CardActuator.Instance.RunFielding(detect);
            int round = LevelManager.Instance.GetRoundCount();
            round = round + 1;
            LevelManager.Instance.SetRoundCount(round);
        }
    }

    private void OnResult(int fieldResult)
    {
        if (finishTimeActive)
        {
            return;
        }

        EventManager.Fire_onExplosion();

        // Cevabın Çek Edilmesi
        int PullResult = LevelManager.Instance.GetResult();
        int levelIndex = LevelManager.Instance.GetLevelIndex();
        int setLevelRhytmic = levelIndex > 10 ? -1 : levelIndex;
        if (PullResult == fieldResult)
        {
            correctCont++;
            EventManager.Fire_onCorrect();
            AudioManager.Instance.PlayCorrectShootingClip();
            LevelManager.Instance.AddBlackList(levelIndex, fieldResult);
            RhythmicManager.Instance.ActiveRhytmic(setLevelRhytmic, fieldResult.ToString());
        }
        else
        {
            wrondCont++;
            EventManager.Fire_onWrong();
            AudioManager.Instance.PlayWrongShootingClip();
            LevelManager.Instance.AddBlackList(levelIndex, fieldResult);
        }

        OnStatus();
    }


    private void OnStatus()
    {
        // CAN SIFIR OLDU ISE
        int heart = HeardVisualizer.Instance.GetSkore();
        if (heart == 0)
        {
            SetStatus("Heart");
            return;
        }

        int stamp = LevelManager.Instance.GetStampCount();
        int round = LevelManager.Instance.GetRoundCount();
        if (stamp == round)
        {
            if (correctCont >= wrondCont)
            {
                SetStatus("NewLevel");
            }
            else
            {
                SetStatus("RepeatLevel");
            }

            return;
        }

        PumpActuator.Instance.RunPumping();
        PullActuator.Instance.RunPulling();
    }

    private void FinishedTime()
    {
        // SÜRE BİTTİ
        finishTimeActive = true;
        SetStatus("Timer");
    }

    private void SetStatus(string status)
    {
        GamePaused();
        CardActuator.Instance.FinishCarding();

        int levelIndex = LevelManager.Instance.GetLevelIndex();
        switch (status)
        {
            case "Timer":
                levelIndex = levelIndex - 1;
                if (levelIndex < 0) levelIndex = 0;
                PopupVisualizer.Instance.ActiveTimer();
                break;
            case "Heart":
                levelIndex = levelIndex - 1;
                if (levelIndex < 0) levelIndex = 0;
                PopupVisualizer.Instance.ActiveHeart();
                break;
            case "NewLevel":
                levelIndex = levelIndex + 1;
                if (levelIndex < 0) levelIndex = 0;
                PopupVisualizer.Instance.ActivePopup(correctCont, wrondCont);
                MTextVisualizer.Instance.SetCorrectLevel();
                break;
            case "RepeatLevel":
                levelIndex = levelIndex + 0;
                if (levelIndex < 0) levelIndex = 0;
                PopupVisualizer.Instance.ActivePopup(correctCont, wrondCont);
                MTextVisualizer.Instance.SetWrongLevel();
                break;
            case "Restart":
                levelIndex = 0;
                if (levelIndex < 0) levelIndex = 0;
                PopupVisualizer.Instance.ActiveRestart();
                RhythmicManager.Instance.RestartRhytmic();
                break;
        }

        wrondCont = 0;
        correctCont = 0;
        LevelManager.Instance.SetRoundCount(0);
        LevelManager.Instance.ClearBlackList();
        LevelManager.Instance.SetLevelIndex(levelIndex);
        StarVisualizer.Instance.SetCount(60);
        HeardVisualizer.Instance.SetSkore(3);
    }

    private void GamePaused()
    {
        EventManager.Fire_onSwipeLock(true);
        StarVisualizer.Instance.CounterCover();
        CardActuator.Instance.PauseGame();
    }

    private void GameStart()
    {
        CardActuator.Instance.ResumeGame();
        StarVisualizer.Instance.CounterLauncher();
        EventManager.Fire_onSwipeLock(false);
    }

    private void UITrigger(string value)
    {
        switch (value)
        {
            case Constants.Settings:
                GamePaused();
                UIVisualizer.Instance.onClick_Setting();
                UIVisualizer.Instance.SetUILocked(false);
                break;
            case Constants.Clue:
                GamePaused();
                UIVisualizer.Instance.onClick_Clue();
                UIVisualizer.Instance.SetUILocked(false);
                break;
            case Constants.Closed:
                UIVisualizer.Instance.onClick_Closed();
                UIVisualizer.Instance.SetUILocked(true);
                GameStart();
                break;
            case Constants.Restart:
                UIVisualizer.Instance.onClick_Restart();
                UIVisualizer.Instance.SetUILocked(true);
                SetStatus("Restart");
                break;
            case Constants.Sound:
                UIVisualizer.Instance.onClick_Sound();
                break;
            case Constants.Achievements:
                UIVisualizer.Instance.onClick_Achievements();
                break;
            case Constants.Result:
                UIVisualizer.Instance.onClick_Result();
                break;
            case Constants.Back:
                UIVisualizer.Instance.onClick_Back();
                break;
            case Constants.Info:
                UIVisualizer.Instance.onClick_Info();
                break;
            case Constants.Rhythmic:
                UIVisualizer.Instance.onClick_Rhythmic();
                break;
            case Constants.Played:
                UIVisualizer.Instance.onClick_Played();
                break;
            case Constants.Preferences:
                UIVisualizer.Instance.onClick_Preferences();
                break;
            case "CloseTutorial":
                bool returnGame = UIVisualizer.Instance.onTutorial_Clue();
                if (returnGame)
                {
                    UIVisualizer.Instance.onClick_Closed();
                    UIVisualizer.Instance.SetUILocked(true);
                    GameStart();
                }
                break;
            case "ClosePopup":
                if (!LevelManager.Instance.GetLevelProcess())
                {
                    PopupVisualizer.Instance.EndGame();
                    Invoke("EndGame", 3f);
                    return;
                }

                int rotationSpeed = LevelManager.Instance.GetRotationSpeed();
                CardActuator.Instance.SetSpeed(rotationSpeed);
                SpeedVisualizer.Instance.SetSliderValue((int)rotationSpeed);

                PopupVisualizer.Instance.PassivePopup();
                CardActuator.Instance.StartCarding();
                PumpActuator.Instance.RunPumping();
                PullActuator.Instance.RunPulling();

                finishTimeActive = false;
                GameStart();
                break;
        }
    }

    private void EndGame()
    {
        PopupVisualizer.Instance.PassivePopup();
        UIVisualizer.Instance.onClick_EndGame();
        UIVisualizer.Instance.SetUILocked(false);
    }
}