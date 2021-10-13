using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopUpPanel : MonoBehaviour
{

    private void OnEnable()
    {
        OpenedEvents();
    }
    private void OnDisable()
    {
        ClosedEvents();
    }
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    public virtual void OpenedEvents()
    {
        Debug.Log("PANEL OPENED");
    }
    public virtual void ClosedEvents()
    {
        Debug.Log("PANEL CLOSED");
    }


}
