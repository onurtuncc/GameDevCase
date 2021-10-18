using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    
    [SerializeField] private StartPanel startPanel;
    [SerializeField] private FailPanel failPanel;
    [SerializeField] private WinPanel winPanel;
    [SerializeField] private RampPanel rampPanel;



    private void OnEnable()
    {
        ObjectPit.OnLevelFailed += LevelFailedUI;
        Basket.OnLevelCompleted += LevelCompletedUI;
        Basket.OnRampEnter += RampUI;
        
    }
    private void OnDestroy()
    {
        ObjectPit.OnLevelFailed -= LevelFailedUI;
        Basket.OnLevelCompleted -= LevelCompletedUI;
        Basket.OnRampEnter -= RampUI;
    }
    private void Start()
    {
        startPanel.Show();
        
    }
    public void StartGameplayUI()
    {
        startPanel.PassToGamePlay();

    }
    public void RampUI(RampController rampController)
    {
        rampPanel.Show();
        rampPanel.SetFillController(rampController);
    }
    private void LevelFailedUI(ObjectPit objectPit)
    {
        startPanel.Hide();
        rampPanel.Hide();
        failPanel.PitToContinue=objectPit;
        failPanel.Show();
        
    }
    public void LevelBeginUI()
    {
        failPanel.Hide();
        rampPanel.Hide();
        winPanel.Hide();
        startPanel.Show();
      

    }
    
    private void LevelCompletedUI()
    {
        startPanel.Hide();
        rampPanel.Hide();
        winPanel.Show();
    }
    
}
