using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private static ScoreManager instance;
    public static ScoreManager ScoreManagerInstance
    {
        get
        {
            
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private float score;
    public float Score { get { return score; } set { score = value; } }


    void Awake()
    {
        if (instance == null)
        {
            ScoreManagerInstance = this;
        }
        score = 0;
    }
    public void AddLevelScore()
    {
        PlayerPrefController.Instance.AddGold((int)score);
    }

    
}
