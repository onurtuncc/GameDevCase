﻿using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    public Level[] levels;
    private int currentLevel;
    private int levelToCreate;
    private int levelToReplay;
    private LevelCreator levelCreator;
    
    private void Start()
    {
        levelCreator = GetComponent<LevelCreator>();
        CreateFirst2Level();

    }
    public void CreateFirst2Level()
    {
      
        
        levelToReplay = PlayerPrefs.GetInt("lastLevelIndex",0);
        levelToCreate = PlayerPrefs.GetInt("nextLevelIndex",1);
       
        levelCreator.CreateLevel(levels[levelToReplay], false);
        levelCreator.CreateLevel(levels[levelToCreate], false);
 
    }
    
    public void PickLevelAndCreate(bool isReplay)
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        Debug.Log("Current level is:" + currentLevel);
        if (!isReplay)
        {
            
            if (currentLevel >= levels.Length)
            {
                //Debug.Log("Random Level");
                levelToReplay = levelToCreate;
                levelToCreate = Random.Range(0, levels.Length);
               
            }
            else
            {
                levelToCreate = currentLevel;
                levelToReplay = levelToCreate - 1;

            }
            player.position = Vector3.zero;
            levelCreator.CreateLevel(levels[levelToCreate],isReplay);
        }
        else
        {
            
            player.position = Vector3.zero;
            levelCreator.CreateLevel(levels[levelToReplay],isReplay);
        }
        PlayerPrefs.SetInt("lastLevelIndex", levelToReplay);
        PlayerPrefs.SetInt("nextLevelIndex", levelToCreate);
    }
    
}
