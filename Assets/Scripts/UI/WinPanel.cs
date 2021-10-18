using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WinPanel : PopUpPanel
{
    [SerializeField] private TMP_Text goldText;

    public override void Show()
    {
        goldText.text = ScoreManager.ScoreManagerInstance.Score.ToString();
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
