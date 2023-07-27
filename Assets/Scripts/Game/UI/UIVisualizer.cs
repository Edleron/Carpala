namespace Game.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;
    using Game.SOLevel;
    using Game.PlayerPrefs;

    public class UIVisualizer : MonoBehaviour
    {
        public static UIVisualizer Instance { get; private set; }

        public List<Transform> ButtonList = new List<Transform>();
        public List<GameObject> GamePanelList = new List<GameObject>();
        public List<GameObject> MenuPanelList = new List<GameObject>();
        public List<GameObject> TutorialsPanelList = new List<GameObject>();
        public PlayerPrefsManager pPrefManager;
        [HideInInspector] public bool locked = false;

        private void Awake()
        {
            Instance = this;
            ButtonListNameSet();
        }

        private void ButtonListNameSet()
        {
            // GAME
            ButtonList[0].name = Constants.Settings;
            ButtonList[1].name = Constants.Clue;

            // TOP Menu
            ButtonList[2].name = Constants.Closed;
            ButtonList[3].name = Constants.Achievements;
            ButtonList[4].name = Constants.Sound;
            ButtonList[5].name = Constants.Back;

            // TOP Menu
            ButtonList[6].name = Constants.Info;
            ButtonList[7].name = Constants.Rhythmic;
            ButtonList[8].name = Constants.Played;
            ButtonList[9].name = Constants.Preferences;
        }

        private void OnEnable()
        {
            EventManager.onUITrigger += UITrigger;
        }

        private void OnDisable()
        {
            EventManager.onUITrigger -= UITrigger;
        }

        public void DownTutorials()
        {
            GamePanelList[0].SetActive(false);
            GamePanelList[1].SetActive(true);
            GamePanelList[2].SetActive(true);
            GamePanelList[3].SetActive(false);
            TutorialsPanelList[0].SetActive(true);
        }

        public void UpTutorials()
        {
            GamePanelList[0].SetActive(false);
            GamePanelList[1].SetActive(true);
            GamePanelList[2].SetActive(true);
            GamePanelList[3].SetActive(false);
            TutorialsPanelList[0].SetActive(false);
            TutorialsPanelList[1].SetActive(true);
        }

        private void UITrigger(string value)
        {
            if (locked)
            {
                switch (value)
                {
                    case Constants.Settings:
                        GamePanelList[0].SetActive(false);
                        GamePanelList[1].SetActive(true);
                        GamePanelList[2].SetActive(false);
                        GamePanelList[3].SetActive(false);

                        MenuPanelList[0].SetActive(true);
                        MenuPanelList[1].SetActive(false);
                        MenuPanelList[2].SetActive(false);
                        MenuPanelList[3].SetActive(false);
                        MenuPanelList[4].SetActive(false);
                        break;
                    case Constants.Clue:
                        GamePanelList[0].SetActive(false);
                        GamePanelList[1].SetActive(true);
                        GamePanelList[2].SetActive(false);
                        GamePanelList[3].SetActive(false);

                        MenuPanelList[0].SetActive(false);
                        MenuPanelList[1].SetActive(false);
                        MenuPanelList[2].SetActive(false);
                        MenuPanelList[3].SetActive(true);
                        MenuPanelList[4].SetActive(false);
                        break;
                    case Constants.Closed:
                        GamePanelList[0].SetActive(true);
                        GamePanelList[1].SetActive(false);
                        GamePanelList[2].SetActive(false);
                        GamePanelList[3].SetActive(true);

                        MenuPanelList[0].SetActive(true);
                        MenuPanelList[1].SetActive(false);
                        MenuPanelList[2].SetActive(false);
                        MenuPanelList[3].SetActive(false);
                        MenuPanelList[4].SetActive(false);
                        break;
                    case Constants.Achievements:
                        MenuPanelList[0].SetActive(false);
                        MenuPanelList[1].SetActive(false);
                        MenuPanelList[2].SetActive(false);
                        MenuPanelList[3].SetActive(false);
                        MenuPanelList[4].SetActive(false);

                        GamePanelList[0].SetActive(true);
                        GamePanelList[1].SetActive(false);
                        GamePanelList[2].SetActive(false);
                        GamePanelList[3].SetActive(true);

                        EventManager.Fire_onEndLevel();
                        pPrefManager.SaveHeart(5);
                        pPrefManager.SaveLevel(0);
                        break;
                    case Constants.Sound:
                        // Todo
                        break;
                    case Constants.Back:
                        MenuPanelList[0].SetActive(true);
                        MenuPanelList[1].SetActive(false);
                        MenuPanelList[2].SetActive(false);
                        MenuPanelList[3].SetActive(false);
                        MenuPanelList[4].SetActive(false);
                        break;
                    case Constants.Info:
                        MenuPanelList[0].SetActive(false);
                        MenuPanelList[1].SetActive(true);
                        MenuPanelList[2].SetActive(false);
                        MenuPanelList[3].SetActive(false);
                        MenuPanelList[4].SetActive(false);
                        break;
                    case Constants.Rhythmic:
                        MenuPanelList[0].SetActive(false);
                        MenuPanelList[1].SetActive(false);
                        MenuPanelList[2].SetActive(true);
                        MenuPanelList[3].SetActive(false);
                        MenuPanelList[4].SetActive(false);
                        break;
                    case Constants.Played:
                        MenuPanelList[0].SetActive(false);
                        MenuPanelList[1].SetActive(false);
                        MenuPanelList[2].SetActive(false);
                        MenuPanelList[3].SetActive(true);
                        MenuPanelList[4].SetActive(false);
                        break;
                    case Constants.Preferences:
                        MenuPanelList[0].SetActive(false);
                        MenuPanelList[1].SetActive(false);
                        MenuPanelList[2].SetActive(false);
                        MenuPanelList[3].SetActive(false);
                        MenuPanelList[4].SetActive(true);
                        break;
                }
            }

            if (LevelManager.Instance.GetTutorialLevel())
            {
                switch (value)
                {
                    case Constants.BtnPanelSwipeDown:
                        GamePanelList[0].SetActive(true);
                        GamePanelList[1].SetActive(false);
                        GamePanelList[2].SetActive(false);
                        GamePanelList[3].SetActive(true);
                        TutorialsPanelList[0].SetActive(false);
                        EventManager.Fire_onUITrigger("CloseTutorialDownPanel");
                        break;
                    case Constants.BtnPanelSwipeUp:
                        GamePanelList[0].SetActive(true);
                        GamePanelList[1].SetActive(false);
                        GamePanelList[2].SetActive(false);
                        GamePanelList[3].SetActive(true);
                        TutorialsPanelList[1].SetActive(false);
                        EventManager.Fire_onUITrigger("CloseTutorialUpPanel");
                        break;
                }
            }
        }
    }
}