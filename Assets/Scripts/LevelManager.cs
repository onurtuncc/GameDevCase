using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        levelCreator.CreateLevel(levels[0], false);
        levelCreator.CreateLevel(levels[1], false);
    }
    
    public void PickLevelAndCreate(bool isReplay)
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        Debug.Log("Current level is:" + currentLevel);
        if (!isReplay)
        {
            
            if (currentLevel >= levels.Length)
            {
                Debug.Log("Random Level");
                levelToReplay = levelToCreate;
                levelToCreate = Random.Range(0, levels.Length);
                Debug.Log("Level index to replay is"+levelToReplay);
                Debug.Log("Level index to create is" + levelToCreate);


            }
            else
            {
                levelToCreate = currentLevel;
                levelToReplay = levelToCreate - 1;
                Debug.Log("Level index to replay is" + levelToReplay);
                Debug.Log("Level index to create is" + levelToCreate);
            }
            player.position = Vector3.zero;
            levelCreator.CreateLevel(levels[levelToCreate],isReplay);
            



        }
        else
        {
            
            player.position = Vector3.zero;
            levelCreator.CreateLevel(levels[levelToReplay],isReplay);
        }
        



    }
    


}
