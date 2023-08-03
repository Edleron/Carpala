namespace Game.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Game.Road;

    public class UIVisualizer : MonoBehaviour
    {
        public static UIVisualizer Instance { get; private set; }
        public List<Transform> ButtonList = new List<Transform>();
        public List<GameObject> MenuPanelList = new List<GameObject>();
        public GameObject UIMenuPanel;
        public GameObject GameMenuPanel;
        public GameObject SliderPanel;
        public GameObject TutorialsPanel;
        public List<GameObject> TutorialState;

        public GameObject SoundObje;
        private bool SoundState = false;
        public List<Sprite> sTexture = new List<Sprite>();

        private int TutorialCount = 0;
        private bool locked { get; set; }

        private void Awake()
        {
            Instance = this;
            TutorialCount = -1;
            ButtonListNameSet();
        }

        private void Start()
        {
            locked = true;
        }

        public void SetUILocked(bool value)
        {
            locked = value;
        }


        private void ButtonListNameSet()
        {
            // GAME
            ButtonList[0].name = Constants.Settings;
            ButtonList[1].name = Constants.Clue;

            // TOP Menu
            ButtonList[2].name = Constants.Closed;
            ButtonList[3].name = Constants.Restart;
            ButtonList[4].name = Constants.Sound;
            ButtonList[5].name = Constants.Achievements;
            ButtonList[6].name = Constants.Result;
            ButtonList[7].name = Constants.Back;

            // TOP Menu
            ButtonList[8].name = Constants.Info;
            ButtonList[9].name = Constants.Rhythmic;
            ButtonList[10].name = Constants.Played;
            ButtonList[11].name = Constants.Preferences;
        }

        public void onClick_EndGame()
        {
            if (!locked)
            {
                return;
            }

            ButtonList[2].gameObject.SetActive(false);

            UIMenuPanel.SetActive(true);
            SliderPanel.SetActive(false);
            GameMenuPanel.SetActive(false);
            TutorialsPanel.SetActive(false);

            OpenMenuPanel(5);
            RoadVisualizer.Instance.SetRoad();
        }

        public void onClick_Setting()
        {
            if (!locked)
            {
                return;
            }

            ButtonList[2].gameObject.SetActive(true);

            UIMenuPanel.SetActive(true);
            SliderPanel.SetActive(false);
            GameMenuPanel.SetActive(false);
            TutorialsPanel.SetActive(false);

            OpenMenuPanel(0);
        }

        public void onClick_Clue()
        {
            if (!locked)
            {
                return;
            }
            OpenMenuPanel(1);

            UIMenuPanel.SetActive(false);
            SliderPanel.SetActive(false);
            GameMenuPanel.SetActive(false);
            TutorialsPanel.SetActive(true);

            TutorialCount = -1;
            foreach (var item in TutorialState)
            {
                item.SetActive(false);
            }
            onTutorial_Clue();
        }

        public bool onTutorial_Clue()
        {
            TutorialCount++;

            if (TutorialCount == 7)
            {
                UIMenuPanel.SetActive(false);
                SliderPanel.SetActive(true);
                GameMenuPanel.SetActive(true);
                TutorialsPanel.SetActive(false);

                return true;
            }

            SliderPanel.SetActive(TutorialCount == 4 ? true : false);

            for (var i = 0; i < TutorialState.Count; i++)
            {
                TutorialState[i].SetActive(false);
                if (i == TutorialCount)
                {
                    TutorialState[i].SetActive(true);
                }
            }

            return false;
        }

        public void onClick_Closed()
        {

            UIMenuPanel.SetActive(false);
            SliderPanel.SetActive(true);
            GameMenuPanel.SetActive(true);
            TutorialsPanel.SetActive(false);

            OpenMenuPanel(0);
        }

        public void onClick_Restart()
        {

            UIMenuPanel.SetActive(false);
            SliderPanel.SetActive(true);
            GameMenuPanel.SetActive(true);
            TutorialsPanel.SetActive(false);

            OpenMenuPanel(-1);
        }

        public void onClick_Sound()
        {
            // TODO
            SpriteRenderer renderer = SoundObje.GetComponent<SpriteRenderer>();
            AudioSource audioListener = GetComponent<AudioSource>();
            SoundState = !SoundState;
            if (SoundState)
            {
                if (renderer != null && sTexture[1] != null)
                {
                    // Texture'ı değiştirme
                    renderer.sprite = sTexture[1];
                    audioListener.enabled = false;
                }
                else
                {
                    Debug.Log("Hata");
                }
            }
            else
            {
                if (renderer != null && sTexture[0] != null)
                {
                    // Texture'ı değiştirme
                    renderer.sprite = sTexture[0];
                    audioListener.enabled = true;
                }
                else
                {
                    Debug.Log("Hata");
                }
            }
        }
        public void onClick_Achievements()
        {
            OpenMenuPanel(5);
            RoadVisualizer.Instance.SetRoad();
        }
        public void onClick_Result()
        {
            OpenMenuPanel(2);
        }

        public void onClick_Back()
        {
            OpenMenuPanel(0);
        }

        public void onClick_Info()
        {
            OpenMenuPanel(1);
        }

        public void onClick_Rhythmic()
        {
            OpenMenuPanel(2);
        }

        public void onClick_Played()
        {
            OpenMenuPanel(3);
        }

        public void onClick_Preferences()
        {
            OpenMenuPanel(4);
        }

        private void OpenMenuPanel(int value)
        {
            if (value == 0)
            {
                ButtonList[7].gameObject.SetActive(false);
            }
            else
            {
                ButtonList[7].gameObject.SetActive(true);
            }

            for (int i = 0; i < MenuPanelList.Count; i++)
            {
                MenuPanelList[i].SetActive(false);
                if (i == value)
                {
                    MenuPanelList[i].SetActive(true);
                }
            }
        }
    }
}