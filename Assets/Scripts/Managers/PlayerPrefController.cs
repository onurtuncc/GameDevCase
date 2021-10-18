using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefController : MonoBehaviour
{

    public static PlayerPrefController Instance { get; private set; }
    private readonly string lastLevelKey = "lastLevelIndex";
    private readonly string nextLevelKey = "nextLevelIndex";
    private readonly string currentLevelKey = "currentLevel";
    private readonly string totalGoldKey = "totalGold";

    private void Awake()
    {
        Instance = this;
    }
    public void SaveLevelData(int lastLevelIndex,int nextLevelIndex)
    {
        PlayerPrefs.SetInt(lastLevelKey, lastLevelIndex);
        PlayerPrefs.SetInt(nextLevelKey, nextLevelIndex);
    }
    public void AddGold(int amount)
    {
        PlayerPrefs.SetInt(totalGoldKey, GetTotalGold() + amount);
    }
    public int GetTotalGold()
    {
        return PlayerPrefs.GetInt(totalGoldKey, 0);
    }
    public int GetCurrentLevelData()
    {
        return PlayerPrefs.GetInt(currentLevelKey,1);
    }
    public void SetCurrentLevelData(int currentLevel)
    {
        PlayerPrefs.SetInt(currentLevelKey,currentLevel);
    }
    public int[] GetLevelCreateData()
    {
        int[] lastAndNextLevelIndex=new int[2];
        lastAndNextLevelIndex.SetValue(PlayerPrefs.GetInt(lastLevelKey,0), 0);
        lastAndNextLevelIndex.SetValue(PlayerPrefs.GetInt(nextLevelKey,1), 1);
        return lastAndNextLevelIndex;
    }
    public void PassNextLevel()
    {
        SetCurrentLevelData(GetCurrentLevelData() + 1);
    }
}
