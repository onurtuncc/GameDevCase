using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartPanel : PopUpPanel
{
    [SerializeField] private GameObject[] objectsToHide;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text nextLevelText;
    int currentLevel;
    public override void Show()
    {
        SetLevelText();
        foreach (GameObject go in objectsToHide)
        {
            if (go != null)
            {
                go.SetActive(true);
            }
        }
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }
    
    public override void ClosedEvents()
    {
        base.ClosedEvents();
    }
    public override void OpenedEvents()
    {
        base.OpenedEvents();
    }
    public void PassToGamePlay()
    {
        foreach (GameObject go in objectsToHide)
        {
            if (go != null)
            {
                go.SetActive(false);
            }
        }
    }
    private void SetLevelText()
    {
        currentLevel = PlayerPrefController.Instance.GetCurrentLevelData();
        levelText.text = currentLevel.ToString();
        nextLevelText.text = (currentLevel + 1).ToString();
    }
}