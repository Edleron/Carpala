namespace Game.Panel
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PanelChange : MonoBehaviour
    {
        public static PanelChange Instance { get; private set; }

        public List<GameObject> panels;
        private int currentPanelIndex = 0;
        [HideInInspector] public bool locked = false;

        private void Awake()
        {
            Instance = this;
            SetActivePanel(currentPanelIndex);
        }

        public void Prev()
        {
            if (!locked)
            {
                currentPanelIndex--;
                if (currentPanelIndex < 0)
                {
                    currentPanelIndex = panels.Count - 1;
                }
                SetActivePanel(currentPanelIndex);
            }

        }

        public void Next()
        {
            if (!locked)
            {
                currentPanelIndex++;
                if (currentPanelIndex >= panels.Count)
                {
                    currentPanelIndex = 0;
                }
                SetActivePanel(currentPanelIndex);
            }
        }

        private void SetActivePanel(int index)
        {
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].SetActive(i == index);
            }
        }
    }
}