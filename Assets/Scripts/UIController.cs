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

    int currentLevel;


    private void OnEnable()
    {
        ObjectPit.OnLevelFailed += LevelFailedUI;
    }
    private void OnDestroy()
    {
        ObjectPit.OnLevelFailed -= LevelFailedUI;
    }
    private void Awake()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        levelText.text = currentLevel.ToString();
        nextLevelText.text = (currentLevel + 1).ToString();
    }
    public void StartGameplayUI()
    {
        tapButton.SetActive(false);

    }
    private void LevelFailedUI()
    {

        failPanel.SetActive(true);
    }

    
}
