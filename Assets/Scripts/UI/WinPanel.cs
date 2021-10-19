using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WinPanel : PopUpPanel
{
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text totalGoldText;

    public override void Show()
    {
        goldText.text = ScoreManager.ScoreManagerInstance.Score.ToString();
        totalGoldText.text = "Total Gold "+PlayerPrefController.Instance.GetTotalGold().ToString();
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
    
}
