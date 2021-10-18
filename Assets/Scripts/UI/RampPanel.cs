using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RampPanel : PopUpPanel
{
    [SerializeField]private Image fillImage;
    [SerializeField]private TMP_Text percentageText;
    public Text goldText;
    private RampController rc;
    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
        goldText.text = "";
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
        rampController.popUpGoldText = goldText;
    }
    private void Update()
    {
        if(rc!=null)
        fillImage.fillAmount = rc.FillAmount;
        percentageText.text = "%"+(rc.FillAmount*100).ToString();

    }
}
