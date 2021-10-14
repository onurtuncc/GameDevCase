using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailPanel : PopUpPanel
{
    private ObjectPit pitToContinue;
    public ObjectPit PitToContinue {
        get
        {
            return pitToContinue;
        }
        set
        {
            pitToContinue = value;
        }
    }
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
    public void Continue()
    {
        if(pitToContinue!=null)
        pitToContinue.PassThePit();
        Hide();
    }
}
