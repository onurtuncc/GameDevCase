using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    public Level[] levels;
    private int currentLevel;
    private LevelCreator levelCreator;
    
    private void Start()
    {
        levelCreator = GetComponent<LevelCreator>();
        PickLevelAndCreate(false);
    }


    public void PickLevelAndCreate(bool isReplay)
    {
        if (!isReplay)
        {

            currentLevel = PlayerPrefs.GetInt("currentLevel");
            Debug.Log(currentLevel);
            if (currentLevel > levels.Length)
            {

                currentLevel = Random.Range(1, levels.Length + 1);

            }

        }
        player.position = Vector3.zero;
        levelCreator.CreateLevel(levels[currentLevel-1]);

    }
    
    
}
