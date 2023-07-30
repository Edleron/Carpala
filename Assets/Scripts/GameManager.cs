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

    // TUTORIALS ANIMASYON EKLEME

    private void Awake()
    {
        tutorialState = TutorialState.Inactive;
    }

    private void Start()
    {
        // HeardVisualizer.Instance.SetSkore(3);
        // StarVisualizer.Instance.SetCount(60);
        // LevelManager.Instance.SetLevelIndex(0);

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
            if (levelIndex > 10) LevelManager.Instance.LevelGenerate();
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

        PumpActuator.Instance.RunPumping();
        PullActuator.Instance.RunPulling();
    }

    private void TriggerSwipeUp()
    {
        string detect = GreenDetectVisualizer.Instance.GetDetectName();

        if (detect != "None")
        {
            EventManager.Fire_onSwipeLock(true); // False Edilen Yer ^^PullActuator^^^ -> PullAnimActiveFalse

            CardActuator.Instance.RunFielding(detect);
            int round = LevelManager.Instance.GetRoundCount();
            round = round + 1;
            LevelManager.Instance.SetRoundCount(round);
        }
    }

    private void OnResult(int fieldResult)
    {
        EventManager.Fire_onExplosion();

        // SURE SIFIRIN ALTINDA ISE |||||FinishedTİme Çalışacak -> EVENT Sistemine Bağlandı
        if (!finishTimeActive)
        {
            int PullResult = LevelManager.Instance.GetResult();

            // Debug.Log("Result -> " + PullResult);
            // Debug.Log("Field -> " + fieldResult);

            if (PullResult == fieldResult)
            {
                // CEVAP DOĞRU ISE
                int levelIndex = LevelManager.Instance.GetLevelIndex();

                EventManager.Fire_onCorrect();
                AudioManager.Instance.PlayCorrectShootingClip();
                RhythmicManager.Instance.ActiveRhytmic(levelIndex, fieldResult.ToString());

                LevelManager.Instance.AddBlackList(levelIndex);

                int stamp = LevelManager.Instance.GetStampCount();
                int round = LevelManager.Instance.GetRoundCount();
                // YENİ LEVEL GEÇİŞ'E BAK
                if (stamp == round)
                {
                    // YANLIŞ'TA OLSA DOĞRU DA OLSA YENI LEVEL'E GEÇİLECEK
                    if (levelIndex >= 10) LevelManager.Instance.LevelGenerate();
                    levelIndex = levelIndex + 1;

                    // CAN BARI YENILEME
                    int correctStatus = RhythmicManager.Instance.CorrectRhytmic(levelIndex);
                    switch (correctStatus)
                    {
                        case -1:
                            HeardVisualizer.Instance.SetSkore(HeardVisualizer.Instance.GetSkore());
                            break;
                        case 0:
                            HeardVisualizer.Instance.SetSkore(HeardVisualizer.Instance.GetSkore());
                            break;
                        case 1:
                            int skore = HeardVisualizer.Instance.GetSkore();
                            skore = skore + 1;
                            HeardVisualizer.Instance.SetSkore(skore);
                            break;
                    }
                    StarVisualizer.Instance.SetCount(60);

                    LevelManager.Instance.SetRoundCount(0);
                    LevelManager.Instance.SetLevelIndex(levelIndex);
                    LevelManager.Instance.ClearBlackLiset();

                    CardActuator.Instance.FinishCarding();

                    int rotationSpeed = LevelManager.Instance.GetRotationSpeed();
                    CardActuator.Instance.SetSpeed(rotationSpeed);
                    SpeedVisualizer.Instance.SetSliderValue((int)rotationSpeed);
                }

                PumpActuator.Instance.RunPumping();
                PullActuator.Instance.RunPulling();
            }
            else
            {
                // CEVAP YANLIŞ ISE

                EventManager.Fire_onWrong();
                AudioManager.Instance.PlayWrongShootingClip();

                // CAN SIFIRIN ALTINDA ISE
                int heart = HeardVisualizer.Instance.GetSkore();
                if (heart == 0)
                {
                    // AMA CAN = ' 0 LEVEL BAŞA DÖN
                    //AYNI LEVEL'I GONDER
                    int levelIndex = LevelManager.Instance.GetLevelIndex();
                    levelIndex = levelIndex <= 10 ? levelIndex - 1 : levelIndex;

                    LevelManager.Instance.SetRoundCount(0);

                    LevelManager.Instance.SetLevelIndex(levelIndex);
                    LevelManager.Instance.ClearBlackLiset();

                    CardActuator.Instance.FinishCarding();

                    StarVisualizer.Instance.SetCount(60);
                    HeardVisualizer.Instance.SetSkore(3);

                    PumpActuator.Instance.RunPumping();
                    PullActuator.Instance.RunPulling();

                    int rotationSpeed = LevelManager.Instance.GetRotationSpeed();
                    CardActuator.Instance.SetSpeed(rotationSpeed);
                    SpeedVisualizer.Instance.SetSliderValue((int)rotationSpeed);

                    return;
                }

                int stamp = LevelManager.Instance.GetStampCount();
                int round = LevelManager.Instance.GetRoundCount();

                // YENİ LEVEL GEÇİŞ'E BAK
                if (stamp == round)
                {
                    // YANLIŞ'TA OLSA DOĞRU DA OLSA YENI LEVEL'E GEÇİLECEK, YANLIZ LEVEL 10'DAN KÜÇÜKSE BU ŞART SAĞLANACAK
                    int levelIndex = LevelManager.Instance.GetLevelIndex();
                    if (levelIndex >= 10) LevelManager.Instance.LevelGenerate();
                    levelIndex = levelIndex <= 10 ? levelIndex + 1 : levelIndex;

                    StarVisualizer.Instance.SetCount(60);

                    LevelManager.Instance.SetRoundCount(0);
                    LevelManager.Instance.SetLevelIndex(levelIndex);
                    LevelManager.Instance.ClearBlackLiset();

                    CardActuator.Instance.FinishCarding();

                    int rotationSpeed = LevelManager.Instance.GetRotationSpeed();
                    CardActuator.Instance.SetSpeed(rotationSpeed);
                    SpeedVisualizer.Instance.SetSliderValue((int)rotationSpeed);
                }

                PumpActuator.Instance.RunPumping();
                PullActuator.Instance.RunPulling();
            }
        }
    }

    private void FinishedTime()
    {
        // SÜRE BİTTİ
        finishTimeActive = true;

        EventManager.Fire_onSwipeLock(true); // False Edilen Yer ^^PullActuator^^^ -> PullAnimActiveFalse

        int levelIndex = LevelManager.Instance.GetLevelIndex();
        levelIndex = levelIndex <= 10 ? levelIndex : levelIndex;

        LevelManager.Instance.SetRoundCount(0);

        LevelManager.Instance.SetLevelIndex(levelIndex);
        LevelManager.Instance.ClearBlackLiset();

        CardActuator.Instance.FinishCarding();

        StarVisualizer.Instance.SetCount(60);
        HeardVisualizer.Instance.SetSkore(3);

        PumpActuator.Instance.RunPumping();
        PullActuator.Instance.RunPulling();

        int rotationSpeed = LevelManager.Instance.GetRotationSpeed();
        CardActuator.Instance.SetSpeed(rotationSpeed);
        SpeedVisualizer.Instance.SetSliderValue((int)rotationSpeed);

        finishTimeActive = false;
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

                HeardVisualizer.Instance.SetSkore(3);
                StarVisualizer.Instance.SetCount(60);

                LevelManager.Instance.SetLevelIndex(0);
                LevelManager.Instance.SetRoundCount(0);

                CardActuator.Instance.FinishCarding();
                PumpActuator.Instance.RunPumping();
                PullActuator.Instance.RunPulling();

                UIVisualizer.Instance.SetUILocked(true);
                GameStart();
                // TODO
                break;
            case Constants.Sound:
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
            case "CloseBtn":
                bool returnGame = UIVisualizer.Instance.onTutorial_Clue();
                if (returnGame)
                {
                    UIVisualizer.Instance.onClick_Closed();
                    UIVisualizer.Instance.SetUILocked(true);
                    GameStart();
                }
                break;
        }
    }
}