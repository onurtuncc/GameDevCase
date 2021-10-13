using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIController : MonoBehaviour
{
    
    [SerializeField] private StartPanel startPanel;
    [SerializeField] private FailPanel failPanel;
    [SerializeField] private WinPanel winPanel;




    private void OnEnable()
    {
        ObjectPit.OnLevelFailed += LevelFailedUI;
        Basket.OnLevelCompleted += LevelCompletedUI;
        
    }
    private void OnDestroy()
    {
        ObjectPit.OnLevelFailed -= LevelFailedUI;
        Basket.OnLevelCompleted -= LevelCompletedUI;
    }
    private void Start()
    {
        startPanel.Show();
    }
    public void StartGameplayUI()
    {
        startPanel.PassToGamePlay();

    }
    private void LevelFailedUI()
    {
        startPanel.Hide();
        failPanel.Show();
    }
    public void LevelBeginUI()
    {
        failPanel.Hide();
        winPanel.Hide();
        startPanel.Show();
      

    }
    
    private void LevelCompletedUI()
    {
        startPanel.Hide();
        winPanel.Show();
    }
    
}
