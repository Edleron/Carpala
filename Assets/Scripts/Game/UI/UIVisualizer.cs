namespace Game.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;

    public class UIVisualizer : MonoBehaviour
    {
        public List<Transform> ButtonList = new List<Transform>();

        private void Awake()
        {
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

        private void UITrigger(string value)
        {
            Debug.Log(value);
            switch (value)
            {
                case Constants.Settings:
                    break;
                case Constants.Clue:
                    break;
                case Constants.Closed:
                    break;
                case Constants.Achievements:
                    break;
                case Constants.Sound:
                    break;
                case Constants.Back:
                    break;
                case Constants.Info:
                    break;
                case Constants.Rhythmic:
                    break;
                case Constants.Played:
                    break;
                case Constants.Preferences:
                    break;
            }
        }
    }
}