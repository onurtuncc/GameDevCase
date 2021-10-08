using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text nextLevelText;
    [SerializeField] private GameObject tapButton;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject winPanel;

    int currentLevel;


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
        SetLevelText();
    }
    public void StartGameplayUI()
    {
        tapButton.SetActive(false);

    }
    private void LevelFailedUI()
    {

        failPanel.SetActive(true);
    }
    public void LevelBeginUI()
    {
        failPanel.SetActive(false);
        winPanel.SetActive(false);
        tapButton.SetActive(true);
        SetLevelText();

    }
    private void SetLevelText()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        levelText.text = currentLevel.ToString();
        nextLevelText.text = (currentLevel + 1).ToString();
    }
    private void LevelCompletedUI()
    {
        winPanel.SetActive(true);
    }
    
}
