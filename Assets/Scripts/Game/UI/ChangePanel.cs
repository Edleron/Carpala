using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanel : MonoBehaviour
{
    public List<GameObject> panels;
    private int currentPanelIndex = 1;

    private void Awake()
    {
        SetActivePanel(currentPanelIndex);
    }

    public void Prev()
    {
        currentPanelIndex--;
        if (currentPanelIndex < 0)
        {
            currentPanelIndex = panels.Count - 1;
        }
        SetActivePanel(currentPanelIndex);
    }

    public void Next()
    {
        currentPanelIndex++;
        if (currentPanelIndex >= panels.Count)
        {
            currentPanelIndex = 0;
        }
        SetActivePanel(currentPanelIndex);
    }

    private void SetActivePanel(int index)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(i == index);
        }
    }
}
