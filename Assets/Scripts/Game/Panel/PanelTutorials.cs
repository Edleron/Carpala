namespace Game.Panel
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PanelTutorials : MonoBehaviour
    {
        public static PanelTutorials Instance { get; private set; }

        public List<GameObject> panels;

        private void Awake()
        {
            Instance = this;
            panels[0].SetActive(false);
            panels[1].SetActive(false);
        }


        public void DownTutorials(bool value)
        {
            panels[0].SetActive(value);
        }

        public void UpTutorials(bool value)
        {
            panels[1].SetActive(value);
        }
    }
}