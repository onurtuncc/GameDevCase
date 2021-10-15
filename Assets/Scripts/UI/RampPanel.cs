using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RampPanel : PopUpPanel
{
    [SerializeField]private Image fillImage;
    [SerializeField]private TMP_Text percentageText;

    private RampController rc;
    public override void Show()
    {
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
    public void SetFillController(RampController rampController)
    {
        rc = rampController;
    }
    private void Update()
    {
        if(rc!=null)
        fillImage.fillAmount = rc.FillAmount;
        percentageText.text = "%"+(rc.FillAmount*100).ToString();

    }
}
